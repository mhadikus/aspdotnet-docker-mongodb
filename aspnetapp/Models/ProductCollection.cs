namespace aspnetapp.Models
{
    internal class ProductCollection
    {
        public IEnumerable<IProduct> GetProducts() => DatabaseHelper.GetProducts();

        public int Count() => GetProducts().Count();

        public void Insert(IProduct product) => DatabaseHelper.Insert(product);
    }
}