using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace aspnetapp.Models.Mongo
{
    internal class MongoProductCollection
    {
        private static readonly Lazy<IMongoCollection<MongoProduct>> _collection = new(MongoHelper.GetCollection<MongoProduct>());

        private static IMongoQueryable<MongoProduct> Products => _collection.Value.AsQueryable();

        private static IMongoCollection<MongoProduct> Collection => _collection.Value;

        public IEnumerable<IProduct> GetProducts()
        {
            return Products;
        }

        public int Count() { return Products.Count(); }

        public void Insert(Product product)
        {
            Collection.InsertOne(new MongoProduct(product));
        }
    }
}