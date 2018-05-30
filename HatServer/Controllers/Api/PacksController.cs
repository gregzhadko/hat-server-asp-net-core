using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatServer.DAL;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HatServer.Controllers.Api
{
    [Route("api/[controller]")]
    public sealed class PacksController : Controller
    {
        private readonly IPackRepository _packRepository;

        public PacksController(IPackRepository packRepository)
        {
            _packRepository = packRepository;
        }

        // GET: api/<controller>
        [NotNull]
        [HttpGet]
        public List<Pack> GetAll()
        {
            return _packRepository.GetAll().ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id should be greater than 0");
            }

            var pack = await _packRepository.GetFullInfo(id);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var existing = await _packRepository.GetByNameAsync(item.Name);
            if (existing != null)
            {
                return BadRequest($"Pack with name {item.Name} already exists");
            }

            await _packRepository.InsertAsync(item);
            return CreatedAtRoute("Get", new {id = item.Id}, item);
        }

        //TODO: check it and refactor
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNameAndDescription(int id, [CanBeNull] [FromBody] string name, [CanBeNull] [FromBody] string description)
        {
            if (id == 0 || String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(description))
            {
                return BadRequest();
            }

            var pack = await _packRepository.GetAsync(id);
            if (pack == null)
            {
                return NotFound();
            }

            pack.Name = name;
            pack.Description = description;

            await _packRepository.UpdateAsync(pack);

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
