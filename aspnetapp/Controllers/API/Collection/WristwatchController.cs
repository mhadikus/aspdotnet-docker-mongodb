using aspnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Net;

namespace aspnetapp.Controllers.API.Collection
{
    [ApiController]
    [Route("api/collection/wristwatches")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class WristwatchController : ControllerBase
    {
        private readonly IMongoCollection<MongoWristwatch> _wristwatches;

        public WristwatchController()
        {
            _wristwatches = MongoHelper.GetCollection<MongoWristwatch>();
        }

        [HttpGet]
        public IEnumerable<Wristwatch> Get()
        {
            foreach( var wristwatch in _wristwatches.AsQueryable())
            {
                yield return new Wristwatch()
                {
                    Brand = wristwatch.Brand,
                    Model = wristwatch.Model
                };
            }
        }

        [HttpGet("count")]
        public int GetCount()
        {
            var count = _wristwatches
                .AsQueryable()
                .Count();
            return count;
        }

        [HttpPost("insert")]
        public HttpResponseMessage Insert([FromBody] Wristwatch wristwatch)
        {
            HttpStatusCode statusCode;
            string message;

            try
            {
                _wristwatches.InsertOne(new MongoWristwatch()
                {
                    Brand = wristwatch.Brand,
                    Model = wristwatch.Model
                });
                statusCode = HttpStatusCode.Created;
                message = $"Added {wristwatch.Brand} {wristwatch.Model}";
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

    public class Wristwatch
    {
        public required string Brand { get; set; }

        public required string Model { get; set; }
    }
}
