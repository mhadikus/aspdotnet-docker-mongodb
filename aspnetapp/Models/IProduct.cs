namespace aspnetapp.Models
{
    public interface IProduct
    {
        public string Brand { get; }

        public string Model { get; }

        public string? ModelNumber { get; }

        public string? SerialNumber { get; }

        public double Price { get; }

        public double PurchasePrice { get; }

        public DateTime? PurchaseDate { get; }

        public int? Warranty { get; }

        public string? Description { get; }
    }
}
