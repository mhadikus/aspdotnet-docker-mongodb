using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace aspnetapp.Models.Mongo
{
    internal class MongoProduct : Product
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
