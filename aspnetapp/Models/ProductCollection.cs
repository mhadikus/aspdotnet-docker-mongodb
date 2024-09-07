namespace aspnetapp.Models
{
    internal abstract class ProductCollection
    {
        public abstract IEnumerable<IProduct> GetProducts();

        public abstract int Count();

        public abstract void Insert(IProduct product);
    }
}