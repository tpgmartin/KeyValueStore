using KeyValueStore.Models;
using KeyValueStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KeyValueStore.Controllers
{
    [Route("api")]
    public class KeyValueController : Controller
    {
        private readonly IKeyValueStore _keyValueStore;

        public KeyValueController(IKeyValueStore keyValueStore)
        {
            _keyValueStore = keyValueStore;
        }

        // GET: api
        [HttpGet]
        public IEnumerable<KeyValue> Get()
        {
            return _keyValueStore.GetAll();
        }

        // GET api/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var keyValue = _keyValueStore.GetById(id);
            if (keyValue == null)
            {
                return NotFound();
            }

            return Ok(keyValue);
        }

        // POST api
        [HttpPost]
        public IActionResult Post([FromBody]KeyValue value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            var createdKeyValue = _keyValueStore.Add(value);

            return CreatedAtRoute("GetKeyValue", new { id = createdKeyValue.Id }, createdKeyValue);
        }

        // PUT api/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]KeyValue value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var keyValue = _keyValueStore.GetById(id);

            if (keyValue == null)
            {
                return NotFound();    
            }

            value.Id = id;
            _keyValueStore.Update(value);

            return NoContent();
        }

        // DELETE api/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var keyValue = _keyValueStore.GetById(id);
            if (keyValue == null)
            {
                return NotFound();
            }

            _keyValueStore.Delete(keyValue);

            return NoContent();
        }
    }
}
