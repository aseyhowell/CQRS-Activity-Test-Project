using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace Activity.Services.ActivityWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        public IRedisTypedClient<Car> redisClient { get; set; }

        public RedisController()
        {
            redisClient = new RedisClient("host.docker.internal").As<Car>();
        }
        [Route("SeedData")]
        public IActionResult GetAddTestData()
        {
            var cars = new List<Car>();
            var currentCars = redisClient.Lists["urn:cars:current"];
            for (int i = 0; i < 10000; i++)
            {
                var car = new Car
                {
                    Colour = "red",
                    Id = Guid.NewGuid(),
                    Parts = new List<string>()
                };
                for (int j = 0; j < 10; j++)
                {
                  car.Parts.Add(Guid.NewGuid().ToString());  
                }
                cars.Add(car);
            }
            //currentCars.AddRange(cars);
            redisClient.StoreAll(cars);
            return Ok();
        }
        [Route("Data")]
        public IActionResult GetTestData()
        {
            var currentCars = redisClient.Lists["urn:cars:current"];
            var data =redisClient.GetAll();
            return Ok(data);
        }
    }

    public class Car
    { 
        public Guid Id { get; set; }
        public string Colour { get; set; }
        public List<string> Parts { get; set; }
    }
}