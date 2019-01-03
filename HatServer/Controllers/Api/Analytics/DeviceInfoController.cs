using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HatServer.DAL.Interfaces;
using HatServer.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Entities;
using MoreLinq;
using static HatServer.Tools.BadRequestFactory;

namespace HatServer.Controllers.Api.Analytics
{
    [Route("api/analytics/info")]
    public class DeviceInfoController : Controller
    {
        private readonly IDeviceInfoRepository _deviceInfoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeviceInfoController> _logger;

        public DeviceInfoController(IDeviceInfoRepository deviceInfoRepository, IMapper mapper, ILogger<DeviceInfoController> logger)
        {
            _deviceInfoRepository = deviceInfoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostDeviceInfoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return HandleAndReturnBadRequest(ModelState, _logger);
            }

            var info = _mapper.Map<DeviceInfo>(request);

            await _deviceInfoRepository.InsertAsync(info);

            return Ok();
        }

        [HttpGet("unique")]
        public IActionResult GetDistinctDevicesInfos()
        {
            return Ok(_deviceInfoRepository.GetAll()
                .Where(i => !i.DeviceModel.Equals("x86_64", StringComparison.InvariantCultureIgnoreCase)).ToList()
                .OrderBy(d => d.TimeStamp).DistinctBy(d => d.DeviceGuid).OrderByDescending(d => d.TimeStamp).ToList());
        }
    }
}