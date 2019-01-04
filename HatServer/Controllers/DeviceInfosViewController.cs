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

        [HttpGet("Unique")]
        public IActionResult Index()
        {
          var data = _deviceInfoRepository.GetAll()
            .Where(i => !i.DeviceModel.Equals("x86_64", StringComparison.InvariantCultureIgnoreCase)).ToList()
            .OrderBy(d => d.TimeStamp).DistinctBy(d => d.DeviceGuid).OrderByDescending(d => d.TimeStamp).ToList();
          return View(data);
        }

        [HttpGet("DailyUnique")]
        public IActionResult GetInfoForDates()
        {
          var data = _deviceInfoRepository.GetAll()
            .Where(i => !i.DeviceModel.Equals("x86_64", StringComparison.InvariantCultureIgnoreCase)).ToList()
            .OrderBy(d => d.TimeStamp).DistinctBy(d => d.DeviceGuid)
            .Select(d => d.DateTime.Date);

          var grouped = data.GroupBy(d => d.Date);
          var dateDeviceInfos = grouped.OrderByDescending(g => g.Key).Select(g => new DateDeviceInfo(){DateTime = g.Key, Count = g.Count()});

          return View(dateDeviceInfos);
        }
    }

    public class DateDeviceInfo
    {
      public DateTime DateTime { get; set; }
      public int Count { get; set; }

      public string DateString => DateTime.ToString("dd/MM/yyyy");
    }
}
