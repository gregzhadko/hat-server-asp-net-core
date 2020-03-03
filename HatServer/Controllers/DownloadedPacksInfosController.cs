using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HatServer.DAL.Interfaces;

namespace HatServer.Controllers
{
    [Route("DownloadedPacks")]
    public sealed class DownloadedPacksInfosController : Controller
    {
        private readonly IDownloadedPacksInfoRepository _repository;

        public DownloadedPacksInfosController(IDownloadedPacksInfoRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Returns the table which contains the information about downloaded packs.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var all = await _repository.GetAllAsync();
            var infos = all.OrderByDescending(i => i.DownloadedTime).ToList();
            return View(infos);
        }

        
        [ApiExplorerSettings(IgnoreApi=true)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var downloadedPacksInfo = await _repository.GetAsync(id.Value);
            if (downloadedPacksInfo == null)
            {
                return NotFound();
            }

            return View(downloadedPacksInfo);
        }
    }
}