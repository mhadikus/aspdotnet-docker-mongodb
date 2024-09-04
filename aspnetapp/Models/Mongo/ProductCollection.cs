using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace aspnetapp.Models.Mongo
{
    internal class ProductCollection
    {
        private static readonly Lazy<IMongoCollection<MongoProduct>> _collection = new(MongoHelper.GetCollection<MongoProduct>());

        public IMongoQueryable<MongoProduct> Products => _collection.Value.AsQueryable<MongoProduct>();

        public int Count() { return Products.Count(); }
    }
}