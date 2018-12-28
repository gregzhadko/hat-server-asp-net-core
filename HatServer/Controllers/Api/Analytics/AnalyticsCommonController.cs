using HatServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace HatServer.Controllers.Api.Analytics
{
    [Route("Analytics/Api/Common")]
    public class AnalyticsCommonController : Controller
    {
        private readonly IAnalyticsBusinessLogic _analyticsBusinessLogic;

        public AnalyticsCommonController(IAnalyticsBusinessLogic analyticsBusinessLogic)
        {
            _analyticsBusinessLogic = analyticsBusinessLogic;
        }
        
        // GET
        public IActionResult Index()
        {
            var result = _analyticsBusinessLogic.GetCommonAnalytics();
            return Ok(result);
        }
    }
}