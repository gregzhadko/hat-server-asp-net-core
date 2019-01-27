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
    /// <inheritdoc />
    /// <summary>
    /// Contains API to work with devices information
    /// </summary>
    [Route("api/analytics/info")]
    public class DeviceInfoController : Controller
    {
        private readonly IDeviceInfoRepository _deviceInfoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeviceInfoController> _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="deviceInfoRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public DeviceInfoController(IDeviceInfoRepository deviceInfoRepository, IMapper mapper, ILogger<DeviceInfoController> logger)
        {
            _deviceInfoRepository = deviceInfoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Saves information about a device which were used to play a game
        /// </summary>
        /// <param name="request">Device information body</param>
        /// <response code="200">Device information was saved</response>
        /// <response code="400">Request body is incorrect</response>
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

        /// <summary>
        /// Provide information about all distinct devices ordered by date.
        /// It can be used for analytics
        /// </summary>
        /// <returns>The list of devices</returns>
        [HttpGet("unique")]
        public async Task<IActionResult> GetDistinctDevicesInfos()
        {
            return Ok(_deviceInfoRepository.GetDistinctDevicesInfosExpectedTestsAsync());
        }
    }
}