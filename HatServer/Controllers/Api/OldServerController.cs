using System.Threading.Tasks;
using HatServer.Services;
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
        
        [HttpGet]
        public async Task<IActionResult> GetPacks()
        {
            var response = await _oldServerService.GetPacksAsync();
            return Ok(response);
        }
    }
}