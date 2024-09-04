using MongoDB.Driver;

namespace aspnetapp.Models.Mongo
{
    internal static class MongoHelper
    {
        private const string CollectionName = "my_products";

        private static readonly Lazy<ILogger> _logger = new(CreateLogger);
        private static readonly Lazy<string> _hostName = new(GetHostName);
        private static readonly Lazy<int> _port = new(GetPort);
        private static readonly Lazy<(string, string)> _credentials = new(GetCredentials);
        private static readonly Lazy<string> _databaseName = new(GetDatabaseName());
        private static readonly Lazy<MongoClient> _client = new(CreateMongoClient);

        private static ILogger Logger => _logger.Value;
        private static string HostName => _hostName.Value;
        private static int Port => _port.Value;
        private static (string, string) Credentials => _credentials.Value;
        private static string DatabaseName => _databaseName.Value;
        private static MongoClient Client => _client.Value;

        public static IMongoCollection<T> GetCollection<T>()
        {
            var database = Client.GetDatabase(DatabaseName);
            var collection = database.GetCollection<T>(CollectionName);
            return collection;
        }

        private static MongoClient CreateMongoClient()
        {
            var settings = new MongoClientSettings
            {
                Server = new MongoServerAddress(HostName, Port),
                Credential = MongoCredential.CreateCredential(
                    DatabaseName,
                    Credentials.Item1,
                    Credentials.Item2),
            };
            var client = new MongoClient(settings);
            // var client = new MongoClient("mongodb://mongodb-dev:mongodb-dev@localhost:27017");
            return client;
        }

        private static string GetHostName()
        {
            return GetEnvironmentVariable("MONGODB_HOST", "localhost");
        }

        private static int GetPort()
        {
            var portString = GetEnvironmentVariable("MONGODB_PORT", "27017");
            _ = int.TryParse(portString, out int port);
            return port;
        }

        private static (string, string) GetCredentials()
        {
            var user = GetEnvironmentVariable("MONGODB_USER", "mongodb-dev");
            var pwd = GetEnvironmentVariable("MONGODB_PW", "mongodb-dev");
            return (user, pwd);
        }

        private static string GetDatabaseName()
        {
            return GetEnvironmentVariable("MONGODB_INIT_DATABASE", "my_data");
        }

        private static string GetEnvironmentVariable(string variable, string defaultValue)
        {
            var value = Environment.GetEnvironmentVariable(variable);
            if (!string.IsNullOrEmpty(value))
            {
                Logger.LogInformation("Reading {Variable} from env: {Value}", variable, value);
            }
            else
            {
                Logger.LogWarning("Unable to read {Variable} from env", variable);
            }
            return value ?? defaultValue;
        }

        private static ILogger CreateLogger()
        {
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            return factory.CreateLogger(typeof(MongoHelper));
        }
    }
}
