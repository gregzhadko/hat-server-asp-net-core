using System.Threading.Tasks;
using HatServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace HatServer.Controllers.Api.Analytics
{
    [Route("Api/Analytics")]
    public class AnalyticsController : Controller
    {
        private readonly IAnalyticsBusinessLogic _analyticsBusinessLogic;

        public AnalyticsController(IAnalyticsBusinessLogic analyticsBusinessLogic)
        {
            _analyticsBusinessLogic = analyticsBusinessLogic;
        }
        
        // GET
        [Route("Common")]
        public IActionResult Index()
        {
            var result = _analyticsBusinessLogic.GetCommonAnalytics();
            return Ok(result);
        }

        [HttpGet("Games/{id}")]
        public async Task<IActionResult> GetGame(int id)
        {
            var result = await _analyticsBusinessLogic.GetFullGameByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("Games")]
        public IActionResult GetGames()
        {
            var result = _analyticsBusinessLogic.GetFullGames();
            return Ok(result);
        }
    }
}