using System;
using System.Threading.Tasks;
using HatServer.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HatServer.Controllers.Api
{
    [Route("api/[controller]")]
    public class OldServerController : Controller
    {
        private readonly IOldServerService _oldServerService;

        public OldServerController([FromServices] IOldServerService oldServerService)
        {
            _oldServerService = oldServerService;
        }

        [HttpGet("{*url}")]
        public async Task<IActionResult> Index()
        {
            const string subUri = "/api/oldserver/";
            var uri = HttpContext.Request.GetDisplayUrl();
            var request = uri.Substring(uri.LastIndexOf(subUri, StringComparison.OrdinalIgnoreCase) + subUri.Length);
            var indexOfSlash = request.IndexOf("/", StringComparison.OrdinalIgnoreCase);
            if (indexOfSlash > 0 && Int32.TryParse(request.Substring(0, indexOfSlash), out int port))
            {
                request = request.Substring(indexOfSlash + 1);
                
                return Ok(await _oldServerService.GetResponseAsync(request, port));
            }

            return Ok(await _oldServerService.GetResponseAsync(request, 8081));
        }
    }
}