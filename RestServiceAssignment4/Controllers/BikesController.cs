using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpUnitTestAsignment1;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestServiceAssignment4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikesController : ControllerBase
    {
        public static List<Bicycle> bicycles = new List<Bicycle>()
        {
            new Bicycle("Green", 999.99, 17, 0),
            new Bicycle("Green", 1234, 32, 1),
            new Bicycle("Green", Double.Epsilon, 3, 2),
            new Bicycle("Green", Double.MaxValue, 32, 3)
        };

        public static List<int> numbers = new List<int>();

        // GET: api/<BikesController>
        [HttpGet]
        public IEnumerable<Bicycle> Get()
        {
            return bicycles;
        }

        // GET api/<BikesController>/5
        [HttpGet]
        [Route("{id}")]
        public Bicycle Get(uint id)
        {
            return bicycles.Find(bike => bike.Id == id);
        }

        // POST api/<BikesController>
        [HttpPost]
        public void Post([FromBody] Bicycle value)
        {
            value.Id = (uint)numbers[0];
            bicycles.Add(value);
            numbers.RemoveAt(0);
        }

        // PUT api/<BikesController>/5
        [HttpPut]
        [Route("{id}")]
        public void Put(uint id, [FromBody] Bicycle value)
        {
            Bicycle bike = Get(id);

            bike.Color = value.Color;
            bike.Gear = value.Gear;
            bike.Price = value.Price;
        }

        // DELETE api/<BikesController>/5
        [HttpDelete]
        [Route("{id}")]
        public void Delete(uint id)
        {
            for (int i = 0; i < bicycles.Count; i++)
            {
                if (bicycles[i].Id == id)
                {
                    bicycles.RemoveAt(i);
                    return;
                }
            }
        }

        [HttpPost]
        [Route("generate")]
        public void Generate()
        {
            Random rand = new Random(DateTime.Now.Millisecond);

            int index = rand.Next(0, numbers.Count);

            bicycles.Add(new Bicycle("Green", rand.Next(0, Int32.MaxValue) * rand.NextDouble(), (byte)rand.Next(3,34), (uint)numbers[index]));

            numbers.RemoveAt(index);
        }
    }
}
