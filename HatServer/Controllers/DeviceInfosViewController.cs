using System;
using System.Linq;
using HatServer.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MoreLinq.Extensions;

namespace HatServer.Controllers
{
    [Route("DeviceInfosPacks")]
    public class DeviceInfosViewController : Controller
    {
        private readonly IDeviceInfoRepository _deviceInfoRepository;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="deviceInfoRepository"></param>
        public DeviceInfosViewController(IDeviceInfoRepository deviceInfoRepository)
        {
            _deviceInfoRepository = deviceInfoRepository;
        }

        /// <summary>
        /// Returns the view with the information about all unique devices ordered by date
        /// </summary>
        /// <returns>HTML page</returns>
        [HttpGet("Unique")]
        public IActionResult Index()
        {
            var data = _deviceInfoRepository.GetAll()
                .Where(i => !i.DeviceModel.Equals("x86_64", StringComparison.InvariantCultureIgnoreCase)).ToList()
                .OrderBy(d => d.TimeStamp).DistinctBy(d => d.DeviceGuid).OrderByDescending(d => d.TimeStamp).ToList();
            return View(data);
        }

        /// <summary>
        /// Returns the view with the inforation about unique devices on each day.
        /// </summary>
        /// <returns></returns>
        [HttpGet("DailyUnique")]
        public IActionResult GetInfoForDates()
        {
            var data = _deviceInfoRepository.GetAll()
                .Where(i => !i.DeviceModel.Equals("x86_64", StringComparison.InvariantCultureIgnoreCase)).ToList()
                .OrderBy(d => d.TimeStamp).DistinctBy(d => d.DeviceGuid)
                .Select(d => d.DateTime.Date);

            var grouped = data.GroupBy(d => d.Date);
            var dateDeviceInfos = grouped.OrderByDescending(g => g.Key)
                .Select(g => new DateDeviceInfo {DateTime = g.Key, Count = g.Count()});

            return View(dateDeviceInfos);
        }
    }

    public class DateDeviceInfo
    {
        public DateTime DateTime { private get; set; }
        public int Count { get; set; }

        public string DateString => DateTime.ToString("dd/MM/yyyy");
    }
}