using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace aspnetapp.Models
{
    public class MongoWristwatch
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement]
        public required string Brand { get; set; }

        [BsonElement]
        public required string Model { get; set; }
    }
}
