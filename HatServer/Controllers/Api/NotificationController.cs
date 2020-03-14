using System.Threading.Tasks;
using HatServer.DAL.Interfaces;
using HatServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace HatServer.Controllers.Api
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
        private readonly IBotNotifier _botNotifier;
        private readonly IDownloadedPacksInfoRepository _repository;

        public NotificationController([FromServices] IBotNotifier botNotifier, IDownloadedPacksInfoRepository repository)
        {
            _botNotifier = botNotifier;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> NotificationAboutDailyDownloads()
        {
            var downloadedPacks = await _repository.GetDownloadsForLastHoursAsync(24);
            if (downloadedPacks.Count == 0)
            {
                return Ok();
            }
            
            var response = await _botNotifier.SendInfoAboutDownloadedPacksAsync(downloadedPacks);
            if (response.IsSuccessStatusCode)
            {
                return Ok(response);
            }

            return StatusCode((int) response.StatusCode);
        }
    }
}