using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HatServer.DAL.Interfaces;
using HatServer.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Entities;
using static HatServer.Tools.BadRequestFactory;

namespace HatServer.Controllers.Api
{
    [Route("api/[controller]")]
    public sealed class GamePacksController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGamePackRepository _gamePackRepository;
        private readonly IDownloadedPacksInfoRepository _downloadedPacksInfoRepository;
        private readonly ILogger<GamePacksController> _logger;

        public GamePacksController(IMapper mapper, IGamePackRepository gamePackRepository,
            IDownloadedPacksInfoRepository downloadedPacksInfoRepository, ILogger<GamePacksController> logger)
        {
            _mapper = mapper;
            _gamePackRepository = gamePackRepository;
            _downloadedPacksInfoRepository = downloadedPacksInfoRepository;
            _logger = logger;
        }

        /// <summary>
        /// Gets all packs with common information about them.
        /// This method should be called from the playing device for all pack representation. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var packs = _gamePackRepository.GetAllAsync();
            var result = _mapper.Map<IList<GamePackEmptyResponse>>(packs);
            return Ok(result);
        }

        /// <summary>
        /// Gets the pack with the list of its phrases by id of the pack.
        /// It should be used in case a user decided to download selected pack.
        /// </summary>
        /// <param name="id">Id of the pack to get</param>
        /// <param name="deviceId">Device id of the user. It will be saved and used for analytics in future</param>
        /// <response code="200">The pack info and list of its phrases</response>
        /// <response code="400">Pack with the provided Id doesn't exist</response>
        [HttpGet("{id}", Name = "Get_Game_Pack")]
        public async Task<IActionResult> Get(int id, [FromHeader] string deviceId)
        {
            var pack = await _gamePackRepository.GetAsync(id);
            if (pack == null)
            {
                return HandleAndReturnBadRequest($"Pack with id = {id} wasn't found", _logger);
            }

            if (!String.IsNullOrWhiteSpace(deviceId))
            {
                //It works, but it sends the notification immediately. It is turned off for now.
                //await botNotifier.SendPackDownloadedNotificationAsync(pack);

                var downloadedInfo = new DownloadedPacksInfo{DownloadedTime = DateTime.UtcNow, DeviceId = new Guid(deviceId), GamePackId = id};
                await _downloadedPacksInfoRepository.InsertAsync(downloadedInfo);
            }

            var response = _mapper.Map<GamePackResponse>(pack);
            return Ok(response);
        }

        /// <summary>
        /// Gets the icon of the pack by its id.
        /// </summary>
        /// <param name="id">Id of the pack</param>
        /// <response code="200">The icon of the pack</response>
        /// <response code="400">Pack icon with the provided Id doesn't exist</response>
        [HttpGet("{id}/icon", Name = "Get_icon")]
        public async Task<IActionResult> GetIcon(int id)
        {
            var icon = await _gamePackRepository.GetPackIconAsync(id);
            if (icon == null)
            {
                return NotFound(new ErrorResponse($"Icon with id = {id} wasn't found"));
            }

            return File(icon.Icon, "application/file", "pack_icon_{id}.pdf");
        }
    }
}