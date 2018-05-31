using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatServer.DAL;
using HatServer.DAL.Interfaces;
using HatServer.DTO.Request;
using HatServer.DTO.Response;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Model;
using Utilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HatServer.Controllers.Api
{
    [Route("api/[controller]")]
    public sealed class PhrasesController : Controller
    {
        private readonly IPhraseRepository _phraseRepository;
        private readonly IPackRepository _packRepository;
        private readonly IUserRepository _userRepository;

        public PhrasesController(IPhraseRepository phraseRepository, IPackRepository packRepository, IUserRepository userRepository)
        {
            _phraseRepository = phraseRepository;
            _packRepository = packRepository;
            _userRepository = userRepository;
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

            return Ok(new BasePhraseItemResponse(phrase));
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

            return Ok(new BasePhraseItemResponse(phraseItem));
        }


        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PostPhraseItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existing = await _phraseRepository.GetByNameAsync(request.Phrase.FormatPhrase());
            if (existing != null)
            {
                return BadRequest($"Phrase {request.Phrase} already exists in pack {existing.Pack.Name}");
            }

            var pack = await _packRepository.GetAsync(request.PackId);
            if (pack == null)
            {
                return NotFound("Pack id is incorrect");
            }

            var user = await _userRepository.GetByNameAsync(request.Author);
            if (user == null)
            {
                return NotFound($"User {request.Author} is not found");
            }

            var phrase = request.ToPhraseItem(user).FormatPhrase();
            await _phraseRepository.InsertAsync(phrase);

            return Ok(new BasePhraseItemResponse(phrase));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _phraseRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
