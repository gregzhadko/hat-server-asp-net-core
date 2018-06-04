using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatServer.DAL.Interfaces;
using HatServer.DTO.Request;
using HatServer.DTO.Response;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

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
        public List<BasePhraseItemResponse> Get()
        {
            return _phraseRepository.GetAll().Select(p => new BasePhraseItemResponse(p)).ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{trackId:int}")]
        public async Task<IActionResult> Get(int trackId)
        {
            if (trackId <= 0)
            {
                return BadRequest("TrackId should be greater than 0");
            }

            var phrase = await _phraseRepository.GetLatestByTrackIdAsync(trackId);
            if (phrase == null)
            {
                return NotFound();
            }

            return Ok(new BasePhraseItemResponse(phrase));
        }

        [HttpGet("{phrase}")]
        public async Task<IActionResult> GetByPhrase([CanBeNull] string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
            {
                return BadRequest("Phrase cannot be empty");
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
        public async Task<IActionResult> Post([FromBody] PostPhraseItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

            var existing = await _phraseRepository.GetByNameAsync(phrase.Phrase);
            if (existing != null)
            {
                return BadRequest($"Phrase {request.Phrase} already exists in pack {existing.Pack.Name}");
            }

            var trackId = await _phraseRepository.GetMaxTrackIdAsync();
            phrase.TrackId = trackId + 1;

            await _phraseRepository.InsertAsync(phrase);

            return Ok(new BasePhraseItemResponse(phrase));
        }

        // PUT api/<controller>/5
        [HttpPut("{trackId}")]
        public async Task<IActionResult> Put(int trackId, [FromBody] PutPhraseItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.GetByNameAsync(request.Author);
            if (user == null)
            {
                return NotFound($"User {request.Author} is not found");
            }

            var actual = await _phraseRepository.GetLatestByTrackIdAsync(trackId);
            if (actual == null)
            {
                return NotFound($"There is no phrase with track id {trackId}");
            }

            var conflictedPhrase = await _phraseRepository.GetByNameExceptTrackIdAsync(request.Phrase, trackId);
            if (conflictedPhrase != null)
            {
                return BadRequest($"The phrase with name {request.Phrase} exists in the pack {conflictedPhrase.Pack.Name}");
            }

            var phrase = request.ToPhraseItem(user, actual).FormatPhrase();

            await _phraseRepository.CloseAndInsertAsync(phrase, actual, user.Id);
            //await _phraseRepository.InsertAsync(phrase);

            return Ok(new BasePhraseItemResponse(phrase));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{trackId:int}")]
        public async Task<IActionResult> Delete(int trackId, [FromBody] string author)
        {
            if (trackId < 0)
            {
                return BadRequest("Track id cannot be less than 0");
            }

            var user = await _userRepository.GetByNameAsync(author);
            if (user == null)
            {
                return BadRequest($"There is no user with name {author}");
            }

            var phrase = await _phraseRepository.GetLatestByTrackIdAsync(trackId);
            if (phrase == null)
            {
                return BadRequest($"There is no phrase with trackId '{trackId}'");
            }

            await _phraseRepository.DeleteAsync(phrase, user.Id);

            return Ok();
        }

        //TODO: delete by phrase
    }
}
