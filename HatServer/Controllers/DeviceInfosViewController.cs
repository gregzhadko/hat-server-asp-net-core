using System;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Index()
        {
            var data = await _deviceInfoRepository.GetDistinctDevicesInfosExpectedTestsAsync();
            return View(data);
        }

        /// <summary>
        /// Returns the view with the inforation about unique devices on each day.
        /// </summary>
        /// <returns></returns>
        [HttpGet("DailyUnique")]
        public async Task<IActionResult> GetInfoForDates()
        {
            var infos = await _deviceInfoRepository.GetDistinctDevicesInfosExpectedTestsAsync();
            var data = infos.Select(d => d.DateTime.Date);

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