using System.Threading.Tasks;
using HatServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace HatServer.Controllers.Api.Analytics
{
    /// <inheritdoc />
    /// <summary>
    /// Contains API to work with analytics of games
    /// </summary>
    [Route("Api/Analytics")]
    public class AnalyticsController : Controller
    {
        private readonly IAnalyticsBusinessLogic _analyticsBusinessLogic;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="analyticsBusinessLogic"></param>
        public AnalyticsController(IAnalyticsBusinessLogic analyticsBusinessLogic)
        {
            _analyticsBusinessLogic = analyticsBusinessLogic;
        }
        
        /// <summary>
        /// Provides the common analytics of played games
        /// </summary>
        /// <returns>Response with the common game analytics</returns>
        [HttpGet("Common")]
        public IActionResult Index()
        {
            var result = _analyticsBusinessLogic.GetCommonAnalyticsAsync();
            return Ok(result);
        }

        /// <summary>
        /// Provides information about game by its id
        /// </summary>
        /// <param name="id">Id of the game</param>
        /// <returns>Response with the full information about the game</returns>
        [HttpGet("Games/{id}")]
        public async Task<IActionResult> GetGame(int id)
        {
            var result = await _analyticsBusinessLogic.GetFullGameByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Provides information about all played real games 
        /// </summary>
        /// <returns>Response with the full information of all real games</returns>
        [HttpGet("Games")]
        public IActionResult GetGames()
        {
            var result = _analyticsBusinessLogic.GetFullGamesAsync();
            return Ok(result);
        }
    }
}