namespace aspnetapp.Models.Mongo
{
    internal class MongoProductCollection : ProductCollection
    {
        private readonly MongoHelper _database = new();
        
        public override IEnumerable<IProduct> GetProducts()
        {
            return _database.GetProducts();
        }

        public override int Count() { return GetProducts().Count(); }

        public override void Insert(IProduct product)
        {
            _database.Insert(new MongoProduct(product));
        }
    }
}