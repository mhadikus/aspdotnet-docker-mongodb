namespace aspnetapp.Models
{
    public class Product
    {
        public required string Brand { get; set; }

        public required string Model { get;  set; }

        public string? ModelNumber { get; set; }

        public string? SerialNumber { get; set; }

        public double Price { get; set; }

        public double PurchasePrice { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public int? Warranty { get; set; }

        public string? Description { get; set; }
    }
}
