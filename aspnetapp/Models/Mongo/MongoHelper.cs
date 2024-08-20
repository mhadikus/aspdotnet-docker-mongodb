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
            var host = Environment.GetEnvironmentVariable("MONGODB_HOST");
            if (!string.IsNullOrEmpty(host))
            {
                Console.WriteLine($"info: Reading MongoDB host name from env: {host}");
            }
            else
            {
                Console.WriteLine($"warn: Unable to read MongoDB host name from env");
            }
            return host ?? "localhost";
        }

        private static int GetPort()
        {
            int port = 27017;
            var portString = Environment.GetEnvironmentVariable("MONGODB_PORT");
            if (!string.IsNullOrEmpty(portString))
            {
                _ = int.TryParse(portString, out port);

                Console.WriteLine($"info: Reading MongoDB port from env: {portString}");
            }
            else
            {
                Console.WriteLine($"warn: Unable to read MongoDB port from env");
            }
            return port;
        }

        private static (string, string) GetCredentials()
        {
            var user = Environment.GetEnvironmentVariable("MONGODB_USER");
            if (!string.IsNullOrEmpty(user))
            {
                Console.WriteLine($"info: Reading MongoDB user name from env: {user}");
            }
            else
            {
                user = "mongodb-dev";
                Console.WriteLine($"warn: Unable to read MongoDB user name from env");
            }

            var pwd = Environment.GetEnvironmentVariable("MONGODB_PW");
            if (!string.IsNullOrEmpty(pwd))
            {
                Console.WriteLine($"info: Reading MongoDB credential from env");
            }
            else
            {
                pwd = "mongodb-dev";
                Console.WriteLine($"warn: Unable to read MongoDB credential from env");
            }
            return (user, pwd);
        }

        private static string GetDatabaseName()
        {
            var database = Environment.GetEnvironmentVariable("MONGODB_INIT_DATABASE");
            if (!string.IsNullOrEmpty(database))
            {
                Console.WriteLine($"info: Reading MongoDB database name from env: {database}");
            }
            else
            {
                Console.WriteLine($"warn: Unable to read MongoDB database name from env");
            }
            return database ?? "my_data";
        }
    }
}
