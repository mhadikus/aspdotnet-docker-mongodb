using MongoDB.Bson.Serialization.Attributes;

namespace aspnetapp.Models.Mongo
{
    [method: System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
    public class Product(string brand, string model)
    {
        [BsonElement]
        public required string Brand { get; set; } = brand;

        [BsonElement]
        public required string Model { get; set; } = model;

        [BsonElement]
        public string? ModelNumber { get; set; }

        [BsonElement]
        public string? SerialNumber { get; set; }

        [BsonElement]
        public double Price { get; set; }

        [BsonElement]
        public double PurchasePrice { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? PurchaseDate { get; set; }

        [BsonElement]
        public int? Warranty { get; set; }

        [BsonElement]
        public string? Description { get; set; }
    }
}
