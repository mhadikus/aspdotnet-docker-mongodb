using MongoDB.Driver;

namespace aspnetapp.Models.Mongo
{
    internal static class MongoHelper
    {
        private const string CollectionName = "my_products";

        private static readonly Lazy<string> _hostName = new(GetHostName);
        private static readonly Lazy<int> _port = new(GetPort);
        private static readonly Lazy<(string, string)> _credentials = new(GetCredentials);
        private static readonly Lazy<string> _databaseName = new(GetDatabaseName);
        private static readonly Lazy<MongoClient> _client = new(GetClient);

        public static MongoClient Client => _client.Value;

        private static MongoClient GetClient()
        {
            var settings = new MongoClientSettings
            {
                Server = new MongoServerAddress(_hostName.Value, _port.Value),
                Credential = MongoCredential.CreateCredential(
                    _databaseName.Value,
                    _credentials.Value.Item1,
                    _credentials.Value.Item2),
            };
            var client = new MongoClient(settings);
            // var client = new MongoClient("mongodb://mongodb-dev:mongodb-dev@localhost:27017");
            return client;
        }

        public static IMongoCollection<T> GetCollection<T>()
        {
            var database = Client.GetDatabase(_databaseName.Value);
            var collection = database.GetCollection<T>(CollectionName);
            return collection;
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
                Console.WriteLine($"info: Reading {variable} from env: {value}");
            }
            else
            {
                Console.WriteLine($"warn: Unable to read {variable} from env");
            }
            return value ?? defaultValue;
        }
    }
}
