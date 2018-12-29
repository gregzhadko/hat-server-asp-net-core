using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HatServer.DAL.Interfaces;
using Model.Entities;
using MoreLinq.Extensions;
using Utilities;

namespace HatServer.Controllers
{
  [Route("DeviceInfosPacks")]
    public class DeviceInfosViewController : Controller
    {
      private readonly IDeviceInfoRepository _deviceInfoRepository;

        public DeviceInfosViewController(IDeviceInfoRepository deviceInfoRepository)
        {
          _deviceInfoRepository = deviceInfoRepository;
        }

        [Route("Unique")]
        public IActionResult Index()
        {
          var data = _deviceInfoRepository.GetAll().ToList().OrderBy(d => d.TimeStamp)
            .DistinctBy(d => d.DeviceGuid).OrderByDescending(d => d.TimeStamp).ToList();
          return View(data);
        }
    }
}
