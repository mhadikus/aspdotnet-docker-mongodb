namespace aspnetapp.Models
{
    public class Product
    {
        public required string Brand { get; set; }

        public required string Model { get;  set; }

        public string? ModelNumber { get; set; }

        public double Price { get; set; }
    }
}
