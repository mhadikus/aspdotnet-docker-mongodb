namespace aspnetapp.Models
{
    [method: System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
    public class Product() : IProduct
    {
        public required string Brand { get; set; } = string.Empty;

        public required string Model { get; set; } = string.Empty;

        public string? ModelNumber { get; set; }

        public string? SerialNumber { get; set; }

        public double Price { get; set; }

        public double PurchasePrice { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public int? Warranty { get; set; }

        public string? Description { get; set; }
    }
}
