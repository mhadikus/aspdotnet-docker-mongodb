using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace aspnetapp.Models.Mongo
{
    internal class MongoProduct : IProduct
    {
        [BsonId]
        public ObjectId Id { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
        public MongoProduct(IProduct product)
        {
            Brand = product.Brand;
            Model = product.Model;
            ModelNumber = product.ModelNumber;
            SerialNumber = product.SerialNumber;
            Price = product.Price;
            PurchasePrice = product.PurchasePrice;
            PurchaseDate = product.PurchaseDate?.ToUniversalTime();
            Warranty = product.Warranty;
            Description = product.Description;
        }

        [System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
        public MongoProduct()
        {
            Brand = string.Empty;
            Model = string.Empty;
        }
    }
}
