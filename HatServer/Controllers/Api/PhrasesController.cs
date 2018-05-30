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
    public sealed class PhrasesController : Controller
    {
        private readonly IPhraseRepository _phraseRepository;

        public PhrasesController(IPhraseRepository phraseRepository)
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
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id should be greater than 0");
            }

            var phrase = await _phraseRepository.GetAsync(id);
            if (phrase == null)
            {
                return NotFound();
            }

            return Ok(phrase);
        }

        [HttpGet("{phrase}")]
        public async Task<IActionResult> GetByName([CanBeNull] string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
            {
                return BadRequest("Phrase cannnot be empty");
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
        public async Task<IActionResult> Post([FromBody]PhraseItem phrase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _phraseRepository.InsertAsync(phrase);

            return Ok(phrase);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public Task Delete(int id)
        {
            return _phraseRepository.DeleteAsync(id);
        }
    }
}
