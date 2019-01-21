using System.Threading.Tasks;
using AutoMapper;
using HatServer.DAL.Interfaces;
using HatServer.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Entities;
using static HatServer.Tools.BadRequestFactory;

namespace HatServer.Controllers.Api.Analytics
{
    /// <summary>
    /// Contains API to work with games
    /// </summary>
    [Route("api/analytics/game")]
    public sealed class GameController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly IRoundRepository _roundRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GameController> _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="gameRepository"></param>
        /// <param name="roundRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public GameController(IGameRepository gameRepository, IRoundRepository roundRepository, IMapper mapper, ILogger<GameController> logger)
        {
            _logger = logger;
            _gameRepository = gameRepository;
            _roundRepository = roundRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Saves information about a game. This method should be called from the device.
        /// </summary>
        /// <param name="request">Game data request</param>
        /// <response code="200">Game information was saved</response>
        /// <response code="400">Request body is incorrect</response>
        [HttpPost]
        public async Task<IActionResult> PostGame([FromBody] PostGameRequest request)
        {
            if (!ModelState.IsValid)
            {
                return HandleAndReturnBadRequest(ModelState, _logger);
            }

            var game = _mapper.Map<Game>(request);
            await _gameRepository.InsertAsync(game);
            
            var rounds = await _roundRepository.GetNotAttachedRoundsByGameGuidAsync(game.InGameId);
            foreach (var round in rounds)
            {
                round.GameId = game.Id;
            }
            
            await _roundRepository.SaveChangesAsync();
            
            return Ok();
        }
    }
}