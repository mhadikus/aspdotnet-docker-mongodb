using MongoDB.Bson.Serialization.Attributes;

namespace aspnetapp.Models.Mongo
{
    [method: System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
    public class Product()
    {
        [BsonElement]
        public required string Brand { get; set; } = string.Empty;

        [BsonElement]
        public required string Model { get; set; } = string.Empty;

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
