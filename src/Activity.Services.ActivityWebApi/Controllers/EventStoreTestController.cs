using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Activity.Services.ActivityWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventStoreTestController : ControllerBase
    {
        public IActionResult GetEvents()
        {
            var conn = EventStoreConnection.Create(new Uri("tcp://admin:changeit@127.0.0.1:1113"),
                "InputFromFileConsoleApp");
             conn.ConnectAsync().Wait();
            conn.AppendToStreamAsync("TestStreamName", ExpectedVersion.Any,new EventData(Guid.NewGuid(), "TestEvent",true,null,null)).Wait();
            Console.WriteLine($"Published test events to eventstore");

            return Ok();
        }
    } 
    
}