using KeyValueStore.Models;
using KeyValueStore.Services;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
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
        public IActionResult Get(string key)
        {
            var keyValue = _keyValueStore.GetByKey(key);
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
            byte[] hash;
            using (MD5 md5 = MD5.Create())
            {
                hash = md5.ComputeHash(Encoding.UTF8.GetBytes(value.Value));
            }
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hash.Length; i++)
			{
                sb.Append(hash[i].ToString("x2"));
			}
            value.Id = Guid.NewGuid();
            value.Key = sb.ToString();
            value.LastUpdated = DateTime.Now;
            var createdKeyValue = _keyValueStore.Add(value);

            return CreatedAtRoute("GetKeyValue", createdKeyValue);
        }

        // PUT api/5
        [HttpPut("{id}")]
        public IActionResult Put(string key, [FromBody]KeyValue value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var keyValue = _keyValueStore.GetByKey(key);

            if (keyValue == null)
            {
                return NotFound();    
            }

            _keyValueStore.Update(value);

            return NoContent();
        }

        // DELETE api/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string key)
        {
            var keyValue = _keyValueStore.GetByKey(key);
            if (keyValue == null)
            {
                return NotFound();
            }

            _keyValueStore.Delete(keyValue);

            return NoContent();
        }
    }
}
