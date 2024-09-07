namespace aspnetapp.Models;

public interface IDatabase
{
    public IEnumerable<IProduct> GetProducts();

    void Insert(IProduct product);
}

public interface IDatabaseData
{
    string Provider { get; }
}
