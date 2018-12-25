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
    [Route("api/analytics/round")]
    public class RoundController : Controller
    {
        private readonly IRoundRepository _roundRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GameController> _logger;

        public RoundController(IRoundRepository roundRepository, IGameRepository gameRepository,IMapper mapper, ILogger<GameController> logger)
        {
            _logger = logger;
            _roundRepository = roundRepository;
            _gameRepository = gameRepository;
            _mapper = mapper;
        }
        
        [HttpPost]
        public async Task<IActionResult> PostRound([FromBody] PostRoundRequest request)
        {
            if (!ModelState.IsValid)
            {
                return HandleAndReturnBadRequest(ModelState, _logger);
            }

            var round = _mapper.Map<Round>(request);

            var game = _gameRepository.GetGameByGuid(round.GameGUID);
            if (game != null)
            {
                round.GameId = game.Id;
            }
            
            await _roundRepository.InsertAsync(round);

            return Ok();
        }
    }
}