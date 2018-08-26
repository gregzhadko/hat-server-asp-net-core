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

        // GET: DownloadedPacksInfos
        public IActionResult Index()
        {
            var infos = _repository.GetAll().OrderByDescending(i => i.DownloadedTime).ToList();
            return View(infos);
        }

        // GET: DownloadedPacksInfos/Details/5
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