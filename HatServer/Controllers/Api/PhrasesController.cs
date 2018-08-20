using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatServer.DAL.Interfaces;
using HatServer.DTO.Request;
using HatServer.DTO.Response;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static HatServer.Tools.BadRequestFactory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HatServer.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public sealed class PhrasesController : Controller
    {
        private readonly IPhraseRepository _phraseRepository;
        private readonly IPackRepository _packRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<PhrasesController> _logger;

        public PhrasesController(IPhraseRepository phraseRepository, IPackRepository packRepository,
            IUserRepository userRepository, ILogger<PhrasesController> logger)
        {
            _phraseRepository = phraseRepository;
            _packRepository = packRepository;
            _userRepository = userRepository;
            _logger = logger;
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
                return HandleAndReturnBadRequest("TrackId should be greater than 0", _logger);
            }

            var phrase = await _phraseRepository.GetLatestByTrackIdAsync(trackId);
            if (phrase == null)
            {
                return HandleAndReturnBadRequest($"Phrase with trackId {trackId} wasn't found", _logger);
            }

            return Ok(new BasePhraseItemResponse(phrase));
        }

        [HttpGet("{phrase}")]
        public async Task<IActionResult> GetByPhrase([CanBeNull] string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
            {
                return HandleAndReturnBadRequest("Phrase cannot be empty", _logger);
            }

            var phraseItem = await _phraseRepository.GetByNameAsync(phrase);
            if (phraseItem == null)
            {
                return HandleAndReturnBadRequest($"Phrase {phrase} wasn't found", _logger);
            }

            return Ok(new BasePhraseItemResponse(phraseItem));
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostPhraseItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return HandleAndReturnBadRequest(ModelState, _logger);
            }

            var pack = await _packRepository.GetAsync(request.PackId);
            if (pack == null)
            {
                return HandleAndReturnBadRequest($"Pack with id {request.PackId} wasn't found", _logger);
            }

            var user = await _userRepository.GetByNameAsync(request.Author);
            if (user == null)
            {
                return HandleAndReturnBadRequest($"User with name {request.Author} wasn't found", _logger);
            }

            var phrase = request.ToPhraseItem(user).FormatPhrase();

            var existing = await _phraseRepository.GetByNameAsync(phrase.Phrase);
            if (existing != null)
            {
                return BadRequest(
                    new ErrorResponse($"Phrase {request.Phrase} already exists in pack {existing.Pack.Name}"));
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
                return HandleAndReturnBadRequest(ModelState, _logger);
            }

            var user = await _userRepository.GetByNameAsync(request.Author);
            if (user == null)
            {
                return HandleAndReturnBadRequest($"User {request.Author} is not found", _logger);
            }

            var actual = await _phraseRepository.GetLatestByTrackIdAsync(trackId);
            if (actual == null)
            {
                return HandleAndReturnBadRequest($"There is no phrase with track id {trackId}", _logger);
            }

            if (actual.Version > request.Version)
            {
                var users = _userRepository.GetAll();
                return Conflict(new ErrorResponse("The phrase has a newer version",
                    new BasePhraseItemResponse(actual, users)));
            }

            var conflictedPhrase = await _phraseRepository.GetByNameExceptTrackIdAsync(request.Phrase, trackId);
            if (conflictedPhrase != null)
            {
                return HandleAndReturnBadRequest(
                    $"The phrase with name {request.Phrase} exists in the pack {conflictedPhrase.Pack.Name}", _logger);
            }

            var phrase = await _phraseRepository.UpdatePhraseAsync(request, user, actual);

            return Ok(new BasePhraseItemResponse(phrase));
        }

        [HttpPost]
        [Route("{trackId}/review")]
        public async Task<IActionResult> PostReview(int trackId, [FromBody] PostReviewRequest request)
        {
            if (!ModelState.IsValid)
            {
                return HandleAndReturnBadRequest(ModelState, _logger);
            }

            var user = await _userRepository.GetByNameAsync(request.Author);
            if (user == null)
            {
                return HandleAndReturnBadRequest($"User {request.Author} is not found", _logger);
            }

            var phrase = await _phraseRepository.GetLatestByTrackIdAsync(trackId);
            if (phrase == null)
            {
                return HandleAndReturnBadRequest($"There is no phrase with track id {trackId}", _logger);
            }

            var newPhrase = await _phraseRepository.AddReviewAsync(phrase, user, request);
            return Ok(new BasePhraseItemResponse(newPhrase));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{trackId:int}")]
        public async Task<IActionResult> Delete(int trackId, [FromBody] string author)
        {
            if (trackId < 0)
            {
                return HandleAndReturnBadRequest("Track id cannot be less than 0", _logger);
            }

            var user = await _userRepository.GetByNameAsync(author);
            if (user == null)
            {
                return HandleAndReturnBadRequest($"There is no user with name {author}", _logger);
            }

            var phrase = await _phraseRepository.GetLatestByTrackIdAsync(trackId);
            if (phrase == null)
            {
                return HandleAndReturnBadRequest($"There is no phrase with trackId '{trackId}'", _logger);
            }

            await _phraseRepository.DeleteAsync(phrase, user.Id);

            return Ok();
        }

        //TODO: delete by phrase
    }
}