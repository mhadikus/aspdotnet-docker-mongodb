using Microsoft.AspNetCore.Mvc;
using System.Net;
using aspnetapp.Models.Mongo;
using MongoDB.Driver;

namespace aspnetapp.Controllers.API.Collection
{
    [ApiController]
    [Route("api/collection/products")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ProductController(ILogger<ProductController> logger) : ControllerBase
    {
        private readonly ILogger<ProductController> _logger = logger;

        private static readonly Lazy<IMongoCollection<MongoProduct>> _products = new(MongoHelper.GetCollection<MongoProduct>());

        private static IMongoCollection<MongoProduct> Products => _products.Value;

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            foreach( var product in Products.AsQueryable())
            {
                yield return product;
            }
        }

        [HttpGet("count")]
        public int GetCount()
        {
            var count = Products
                .AsQueryable()
                .Count();
            return count;
        }

        [HttpPost("insert")]
        public HttpResponseMessage Insert([FromBody] Product product)
        {
            HttpStatusCode statusCode;
            string message;

            try
            {
                var purchaseDate = product.PurchaseDate?.ToUniversalTime();
                Products.InsertOne(new MongoProduct(product));
                statusCode = HttpStatusCode.Created;
                message = $"Added {product.Brand} {product.Model}";
            }
            catch (HttpRequestException exception)
            {
                statusCode = exception.StatusCode ?? HttpStatusCode.BadRequest;
                message = exception.Message;
            }

            var response = CreateResponse(statusCode, message);
            return response;
        }


        private static HttpResponseMessage CreateResponse(HttpStatusCode statusCode, string message)
        {
            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(message),
                ReasonPhrase = message
            };
        }
    }
}
