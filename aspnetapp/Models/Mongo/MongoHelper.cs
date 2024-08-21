using MongoDB.Driver;

namespace aspnetapp.Models.Mongo
{
    internal class MongoHelper
    {
        private const string CollectionName = "my_products";

        private readonly string _hostName;
        private readonly int _port;
        private readonly (string, string) _credentials;
        private readonly string _databaseName;
        private readonly MongoClient _client;
        private readonly ILogger _logger;

        public MongoHelper(ILogger logger)
        {
            _logger = logger;
            _hostName = GetHostName();
            _port = GetPort();
            _credentials = GetCredentials();
            _databaseName = GetDatabaseName();
            _client = CreateMongoClient();
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            var database = _client.GetDatabase(_databaseName);
            var collection = database.GetCollection<T>(CollectionName);
            return collection;
        }

        private MongoClient CreateMongoClient()
        {
            var settings = new MongoClientSettings
            {
                Server = new MongoServerAddress(_hostName, _port),
                Credential = MongoCredential.CreateCredential(
                    _databaseName,
                    _credentials.Item1,
                    _credentials.Item2),
            };
            var client = new MongoClient(settings);
            // var client = new MongoClient("mongodb://mongodb-dev:mongodb-dev@localhost:27017");
            return client;
        }

        private string GetHostName()
        {
            return GetEnvironmentVariable("MONGODB_HOST", "localhost");
        }

        private int GetPort()
        {
            var portString = GetEnvironmentVariable("MONGODB_PORT", "27017");
            _ = int.TryParse(portString, out int port);
            return port;
        }

        private (string, string) GetCredentials()
        {
            var user = GetEnvironmentVariable("MONGODB_USER", "mongodb-dev");
            var pwd = GetEnvironmentVariable("MONGODB_PW", "mongodb-dev");
            return (user, pwd);
        }

        private string GetDatabaseName()
        {
            return GetEnvironmentVariable("MONGODB_INIT_DATABASE", "my_data");
        }

        private string GetEnvironmentVariable(string variable, string defaultValue)
        {
            var value = Environment.GetEnvironmentVariable(variable);
            if (!string.IsNullOrEmpty(value))
            {
                _logger.LogInformation("Reading {Variable} from env: {Value}", variable, value);
            }
            else
            {
                _logger.LogWarning("Unable to read {Variable} from env", variable);
            }
            return value ?? defaultValue;
        }
    }
}
