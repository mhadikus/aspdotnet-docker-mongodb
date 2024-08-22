using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace aspnetapp.Models.Mongo
{
    internal class Product
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement]
        public required string Brand { get; set; }

        [BsonElement]
        public required string Model { get; set; }

        [BsonElement]
        public required string ModelNumber { get; set; }

        [BsonElement]
        public required double Price { get; set; }
    }
}
