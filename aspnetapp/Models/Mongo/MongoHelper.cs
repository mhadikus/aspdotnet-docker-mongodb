using MongoDB.Driver;

namespace aspnetapp.Models.Mongo
{
    internal static class MongoHelper
    {
        private const string Host = "localhost";
        private const int Port = 27017;
        private const string User = "mongodb-dev";
        private const string Password = "mongodb-dev";
        private const string DatabaseName = "my_data";
        private const string CollectionName = "my_products";

        private static readonly Lazy<MongoClient> _client = new(GetClient);

        public static MongoClient Client => _client.Value;

        private static MongoClient GetClient()
        {
            var settings = new MongoClientSettings
            {
                Server = new MongoServerAddress(Host, Port),
                Credential = MongoCredential.CreateCredential(DatabaseName, User, Password),
            };
            var client = new MongoClient(settings);
            // var client = new MongoClient("mongodb://mongodb-dev:mongodb-dev@localhost:27017");
            return client;
        }

        public static IMongoCollection<T> GetCollection<T>()
        {
            var database = Client.GetDatabase(DatabaseName);
            var collection = database.GetCollection<T>(CollectionName);
            return collection;
        }
    }
}
