using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS2.Data;
using MS2.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ISiteRepository _repository;
        public OrdersController(ISiteRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _repository.GetAllProducts();
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderController>
        [HttpPost]
        public  ActionResult<OrderEntry> Post(string entry)
        {
            OrderEntry entry1 = new OrderEntry();
            bool val1 = User.Identity.IsAuthenticated;
            //string jsonString;
            //using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            //{
            //    jsonString = await reader.ReadToEndAsync();
            //}
            return CreatedAtAction("GetSamurai", new { id = entry1.Id }, entry1);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
