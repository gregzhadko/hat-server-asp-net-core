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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return await GetDownloadedPacks(1);
        }

        /// <summary>
        /// Returns the table which contains the information about downloaded packs.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{pageNumber:int}")]
        public async Task<IActionResult> Index(int? pageNumber)
        {
            return await GetDownloadedPacks(pageNumber);
        }

        private async Task<IActionResult> GetDownloadedPacks(int? pageNumber)
        {
            if (pageNumber == null || pageNumber <= 0)
            {
                pageNumber = 1;
            }

            var downloadedPacks = await _repository.GetWithPagination(pageNumber.Value);
            return View(downloadedPacks);
        }
    }
}