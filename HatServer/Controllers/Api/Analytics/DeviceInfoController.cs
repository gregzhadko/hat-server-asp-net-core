using System.Threading.Tasks;
using AutoMapper;
using HatServer.DAL.Interfaces;
using HatServer.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;

namespace HatServer.Controllers.Api.Analytics
{
    [Route("api/analytics/info")]
    public class DeviceInfoController : Controller
    {
        private readonly IDeviceInfoRepository _repository;
        private readonly IMapper _mapper;

        public DeviceInfoController(IDeviceInfoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostDeviceInfoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ParseErrors());
            }

            var info = _mapper.Map<DeviceInfo>(request);
            await _repository.InsertAsync(info);
            
            return Ok();
        }
    }
}