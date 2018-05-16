using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatServer.DAL;
using HatServer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HatServer.Controllers.Api
{
    [Route("api/[controller]")]
    public class PacksController : Controller
    {
        private IRepository<Pack> _packRepository;

        public PacksController(IRepository<Pack> packRepository)
        {
            _packRepository = packRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        public List<Pack> GetAll() => _packRepository.GetAll().ToList();

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pack = await _packRepository.GetAsync(id.Value);
            if (pack == null)
            {
                return NotFound();
            }

            return Ok(pack);
        }


        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pack item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _packRepository.InsertAsync(item);
            return CreatedAtRoute("Get", new { id = item.Id }, item);
        }


        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
