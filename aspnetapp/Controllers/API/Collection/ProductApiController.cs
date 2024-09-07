using Microsoft.AspNetCore.Mvc;
using System.Net;
using aspnetapp.Models;

namespace aspnetapp.Controllers.API.Collection
{
    [ApiController]
    [Route("api/collection/products")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ProductApiController(ILogger<ProductApiController> logger) : ControllerBase
    {
        private readonly ILogger<ProductApiController> _logger = logger;

        private readonly Models.Mongo.MongoHelper _database = new();

        [HttpGet]
        public IEnumerable<IProduct> Get()
        {
            foreach( var product in _database.GetProducts())
            {
                yield return product;
            }
        }

        [HttpGet("count")]
        public int GetCount()
        {
            return _database.GetProducts().Count();
        }

        [HttpPost("insert")]
        public HttpResponseMessage Insert([FromBody] Product product)
        {
            HttpStatusCode statusCode;
            string message;

            try
            {
                var purchaseDate = product.PurchaseDate?.ToUniversalTime();
                _database.Insert(product);
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

        [HttpPost("insert_with_params")]
        public HttpResponseMessage InsertWithParams(
            string brand,
            string model,
            string? modelNumber,
            string? serialNumber,
            double? price,
            double? purchasePrice,
            DateTime? purchaseDate,
            int? warranty,
            string? description
        )
        {
            HttpStatusCode statusCode;
            string message;

            try
            {
                _database.Insert(new Product()
                {
                    Brand = brand,
                    Model = model,
                    ModelNumber = modelNumber,
                    SerialNumber = serialNumber,
                    Price = price ?? 0,
                    PurchasePrice = purchasePrice ?? 0,
                    PurchaseDate = purchaseDate?.ToUniversalTime(),
                    Warranty = warranty,
                    Description = description
                });
                statusCode = HttpStatusCode.Created;
                message = $"Added {brand} {model}";
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
