using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace aspnetapp.Models.Mongo
{
    internal class MongoProductCollection : ProductCollection
    {
        private static readonly Lazy<IMongoCollection<MongoProduct>> _collection = new(MongoHelper.GetCollection<MongoProduct>());

        private static IMongoQueryable<MongoProduct> Products => _collection.Value.AsQueryable();

        private static IMongoCollection<MongoProduct> Collection => _collection.Value;

        public override IEnumerable<IProduct> GetProducts()
        {
            return Products;
        }

        public override int Count() { return Products.Count(); }

        public override void Insert(IProduct product)
        {
            Collection.InsertOne(new MongoProduct(product));
        }
    }
}