namespace aspnetapp.Models.Mongo
{
    internal class MongoProductCollection : ProductCollection
    {
        public override IEnumerable<IProduct> GetProducts()
        {
            return DatabaseHelper.GetProducts();
        }

        public override int Count() { return GetProducts().Count(); }

        public override void Insert(IProduct product)
        {
            DatabaseHelper.Insert(product);
        }
    }
}