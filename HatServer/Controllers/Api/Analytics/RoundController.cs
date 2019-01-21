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
    /// <inheritdoc />
    /// <summary>
    /// Contains API to work with rounds
    /// </summary>
    [Route("api/analytics/round")]
    public class RoundController : Controller
    {
        private readonly IRoundRepository _roundRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GameController> _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="roundRepository"></param>
        /// <param name="gameRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public RoundController(IRoundRepository roundRepository, IGameRepository gameRepository,IMapper mapper, ILogger<GameController> logger)
        {
            _logger = logger;
            _roundRepository = roundRepository;
            _gameRepository = gameRepository;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Saves information about played round.
        /// </summary>
        /// <param name="request">Round data request</param>
        /// <response code="200">Round information was saved</response>
        /// <response code="400">Request body is incorrect</response>
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