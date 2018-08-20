using System.Threading.Tasks;
using AutoMapper;
using HatServer.DAL.Interfaces;
using HatServer.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Entities;
using static HatServer.Tools.BadRequestFactory;

namespace HatServer.Controllers.Api.Analytics
{
    [Route("api/analytics/info")]
    public class DeviceInfoController : Controller
    {
        private readonly IDeviceInfoRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeviceInfoController> _logger;

        public DeviceInfoController(IDeviceInfoRepository repository, IMapper mapper, ILogger<DeviceInfoController> logger)
        {
            _repository = repository;
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
            await _repository.InsertAsync(info);
            
            return Ok();
        }
    }
}