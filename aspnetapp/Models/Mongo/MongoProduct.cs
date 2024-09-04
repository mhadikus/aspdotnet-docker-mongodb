using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace aspnetapp.Models.Mongo
{
    public class MongoProduct : Product
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
