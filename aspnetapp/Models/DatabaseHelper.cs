using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace aspnetapp.Models
{
    public static class DatabaseHelper
    {
        private static readonly ILogger _logger = CreateLogger();
        private static readonly Database _database = new();

        public static IEnumerable<IProduct> GetProducts()
        {
            return _database.GetProducts();
        }

        public static void Insert(IProduct product)
        {
            _database.Insert(product);
        }

        private static ILogger CreateLogger()
        {
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            return factory.CreateLogger(typeof(DatabaseHelper));
        }

        private class Database : IDatabase
        {
            private readonly CompositionContainer? _container;

            [Import(typeof(IDatabase))]
            public Lazy<IDatabase, IDatabaseData>? DatabaseImpl { get; set; }

            public Database()
            {
                try
                {
                    // An aggregate catalog that combines multiple catalogs.
                    var catalog = new AggregateCatalog();
                    // Adds all the parts found in the same assembly.
                    catalog.Catalogs.Add(new AssemblyCatalog(typeof(DatabaseHelper).Assembly));

                    // Create the CompositionContainer with the parts in the catalog.
                    _container = new CompositionContainer(catalog);
                    _container.ComposeParts(this);
                }
                catch (Exception exception)
                {
                    _logger.LogError("Composition Error: {Value}", exception.ToString());
                }

                if (DatabaseImpl != null)
                {
                    _logger.LogInformation("Using {Value} provider: {Value}", nameof(Database), DatabaseImpl.Metadata.Provider);
                }
                else
                {
                    _logger.LogWarning("Unable to find MEF import for {Value} provider", nameof(IDatabase));
                }
            }

            public IEnumerable<IProduct> GetProducts()
            {
                return DatabaseImpl != null
                    ? DatabaseImpl.Value.GetProducts()
                    : [];
            }

            public void Insert(IProduct product)
            {
                DatabaseImpl?.Value.Insert(product);
            }
        }
    }
}
