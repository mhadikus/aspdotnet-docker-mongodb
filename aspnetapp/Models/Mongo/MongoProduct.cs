using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace aspnetapp.Models.Mongo
{
    internal class MongoProduct : Product
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
        public MongoProduct(Product product)
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
