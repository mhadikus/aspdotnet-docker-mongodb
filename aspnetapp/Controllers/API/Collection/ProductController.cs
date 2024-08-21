using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Net;
using MongoHelper = aspnetapp.Models.Mongo.MongoHelper;
using MongoProduct = aspnetapp.Models.Mongo.Product;

namespace aspnetapp.Controllers.API.Collection
{
    [ApiController]
    [Route("api/collection/products")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ProductController(ILogger<ProductController> logger) : ControllerBase
    {
        private readonly ILogger<ProductController> _logger = logger;

        private static readonly Lazy<MongoHelper> _mongoHelper = new(new MongoHelper());

        private static readonly Lazy<IMongoCollection<MongoProduct>> _products = new(_mongoHelper.Value.GetCollection<MongoProduct>());

        private static IMongoCollection<MongoProduct> Products => _products.Value;

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            foreach( var product in Products.AsQueryable())
            {
                yield return new Product()
                {
                    Brand = product.Brand,
                    Model = product.Model
                };
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
                Products.InsertOne(new MongoProduct()
                {
                    Brand = product.Brand,
                    Model = product.Model
                });
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

    public class Product
    {
        public required string Brand { get; set; }

        public required string Model { get; set; }
    }
}
