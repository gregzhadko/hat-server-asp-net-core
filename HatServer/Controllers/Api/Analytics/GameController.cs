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
    [Route("api/analytics/game")]
    public sealed class GameController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly IRoundRepository _roundRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GameController> _logger;

        public GameController(IGameRepository gameRepository, IRoundRepository roundRepository, IMapper mapper, ILogger<GameController> logger)
        {
            _logger = logger;
            _gameRepository = gameRepository;
            _roundRepository = roundRepository;
            _mapper = mapper;
        }

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