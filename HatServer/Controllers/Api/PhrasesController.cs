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
using Model.Entities;
using static HatServer.Tools.BadRequestFactory;

namespace HatServer.Controllers.Api
{
    /// <inheritdoc />
    /// <summary>
    /// Contains API method to work with phrases from filler.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public sealed class PhrasesController : Controller
    {
        private readonly IPhraseRepository _phraseRepository;
        private readonly IPackRepository _packRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<PhrasesController> _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="phraseRepository"></param>
        /// <param name="packRepository"></param>
        /// <param name="userRepository"></param>
        /// <param name="logger"></param>
        public PhrasesController(IPhraseRepository phraseRepository, IPackRepository packRepository,
            IUserRepository userRepository, ILogger<PhrasesController> logger)
        {
            _phraseRepository = phraseRepository;
            _packRepository = packRepository;
            _userRepository = userRepository;
            _logger = logger;
        }
        
        /// <summary>
        /// Gets all phrases and its history. The response include user review data as well. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<BasePhraseItemResponse>> Get()
        {
            var phraseItems = await _phraseRepository.GetAllAsync();
            return phraseItems.Select(p => new BasePhraseItemResponse(p)).ToList();
        }

        /// <summary>
        /// Returns the latest (actual) phrase by track id
        /// </summary>
        /// <param name="trackId"></param>
        /// <response code="400">Provided track id is incorrect: there is no phrase with this track id</response>
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

        /// <summary>
        /// Returns the first phrase from the database which match the input string.
        /// </summary>
        /// <param name="phrase"></param>
        /// <response code="400">There is no such phrase</response>
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


        /// <summary>
        /// Creates and saves a new phrase by data provided in the body
        /// </summary>
        /// <param name="request">A body of the request</param>
        /// <response code="200">Created phrase</response>
        /// <response code="400">Request is incorrect</response>
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

            phrase.Track = new Track();

            await _phraseRepository.InsertAsync(phrase);

            return Ok(new BasePhraseItemResponse(phrase));
        }

        /// <summary>
        /// Update phrase with corresponding track id.
        /// </summary>
        /// <param name="trackId">Track id of the phrase to update</param>
        /// <param name="request">New values of the phrase</param>
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
            
            if (actual.Version >= request.Version)
            {
                var users = await _userRepository.GetAllAsync();
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

        /// <summary>
        /// Update review state of the phrase
        /// </summary>
        /// <param name="trackId">Track id of the phrase to review</param>
        /// <param name="request">Information about review state</param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes phrase from the database
        /// </summary>
        /// <param name="trackId">Track id of the phrase to delete</param>
        /// <param name="author">The name of the user who wanted to delete the phrase</param>
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
    }
}