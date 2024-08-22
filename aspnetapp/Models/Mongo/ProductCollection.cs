using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace aspnetapp.Models.Mongo
{
    internal class ProductCollection
    {
        private static readonly Lazy<MongoHelper> _mongoHelper = new(new MongoHelper());

        private static readonly Lazy<IMongoCollection<Product>> _collection = new(_mongoHelper.Value.GetCollection<Product>());

        public IMongoQueryable<Product> Products => _collection.Value.AsQueryable<Product>();

        public int Count() { return Products.Count(); }
    }
}