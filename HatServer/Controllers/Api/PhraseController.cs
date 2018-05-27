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
    public sealed class PhraseController : Controller
    {
        private readonly IPhraseRepository _phraseRepository;

        public PhraseController(IPhraseRepository phraseRepository)
        {
            _phraseRepository = phraseRepository;
        }

        // GET: api/<controller>
        [NotNull]
        [HttpGet]
        public IList<PhraseItem> Get()
        {
            return _phraseRepository.GetAll().ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var phrase = await _phraseRepository.GetAsync(id);
            if (phrase == null)
            {
                return NotFound();
            }

            return Ok(phrase);
        }

        [HttpGet("name/{phrase}")]
        public async Task<IActionResult> GetByName([CanBeNull] string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
            {
                return BadRequest();
            }

            var phraseItem = await _phraseRepository.GetByNameAsync(phrase);
            if (phraseItem == null)
            {
                return NotFound();
            }

            return Ok(phraseItem);
        }


        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]PhraseItem phrase)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public Task Delete(int id) => _phraseRepository.DeleteAsync(id);
    }
}
