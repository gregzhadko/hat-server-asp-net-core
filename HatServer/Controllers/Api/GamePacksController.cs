using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HatServer.DAL.Interfaces;
using HatServer.DTO.Response;
using HatServer.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model.Entities;

namespace HatServer.Controllers.Api
{
    //[Authorize]
    [Route("api/[controller]")]
    public sealed class GamePacksController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IGamePackRepository _gamePackRepository;
        private readonly IDownloadedPacksInfoRepository _downloadedPacksInfoRepository;

        public GamePacksController(IConfiguration configuration, IMapper mapper, IGamePackRepository gamePackRepository,
            IDownloadedPacksInfoRepository downloadedPacksInfoRepository)
        {
            _configuration = configuration;
            _mapper = mapper;
            _gamePackRepository = gamePackRepository;
            _downloadedPacksInfoRepository = downloadedPacksInfoRepository;
        }

        // GET api/<controller>
        [HttpGet]
        public IActionResult GetAll()
        {
            var packs = _gamePackRepository.GetAll();
            var result = _mapper.Map<IList<GamePackEmptyResponse>>(packs);
            return Ok(result);
            
//            var result = new List<GamePack>();
//            var files = Directory.GetFiles(Constants.PacksFolder, "*.json");
//            foreach (var pack in files.Select(s => System.IO.File.ReadAllText(s, Encoding.UTF8))
//                .Select(JsonConvert.DeserializeObject<GamePack>))
//            {
//                pack.Count = pack.Phrases.Count;
//                pack.Phrases = null;
//                result.Add(pack);
//            }
//
//            return Ok(result);
        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "Get_Game_Pack")]
        public async Task<IActionResult> Get(int id, [FromHeader]string deviceId)
        {
            var pack = await _gamePackRepository.GetAsync(id);
            if (pack == null)
            {
                return BadRequest(new ErrorResponse($"Pack with id = {id} wasn't found"));
            }

            if (!String.IsNullOrWhiteSpace(deviceId))
            {
                var notifier = new BotNotifier(_configuration, _downloadedPacksInfoRepository);
#pragma warning disable 4014
                notifier.SendDownloadedNotificationAsync(pack);
#pragma warning restore 4014
                
                var downloadedInfo = new DownloadedPacksInfo{DownloadedTime = DateTime.UtcNow, DeviceId = new Guid(deviceId), GamePackId = id};
                await _downloadedPacksInfoRepository.InsertAsync(downloadedInfo);
            }

            var response = _mapper.Map<GamePackResponse>(pack);
            return Ok(response);
            
//            var file = Directory.GetFiles(Constants.PacksFolder, "*.json")
//                .FirstOrDefault(f => f.EndsWith($"{id}.json", StringComparison.Ordinal));
//            if (file == null)
//            {
//                return BadRequest(new ErrorResponse($"Pack with id = {id} wasn't found"));
//            }
//
//            var result = await System.IO.File.ReadAllTextAsync($"{file}");
//            return Ok(result);
        }

        // GET api/<controller>/5
        [HttpGet("{id}/icon", Name = "Get_icon")]
        public async Task<IActionResult> GetIcon(int id)
        {
            var icon = await _gamePackRepository.GetPackIcon(id);
            if (icon == null)
            {
                return BadRequest(new ErrorResponse($"Pack with id = {id} wasn't found"));
            }

            return File(icon.Icon, "application/file", "pack_icon_{id}.pdf");

//            var file = Directory.GetFiles(Constants.PacksFolder, "*.pdf")
//                .FirstOrDefault(f => f.EndsWith($"pack_icon_{id}.pdf", StringComparison.Ordinal));
//            if (file == null)
//            {
//                return BadRequest(new ErrorResponse($"Pack with id = {id} wasn't found"));
//            }
//
//            var fileStream = new FileStream(file, FileMode.Open);
//            return File(fileStream, "application/file", "pack_icon_{id}.pdf");
        }
    }
}