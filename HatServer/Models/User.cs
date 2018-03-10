using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatServer.Models
{
    public class User
    {
        public int Id { get; set; }
        public Guid DeviceId { get; set; }
        public string DeviceModel { get; set; }
        public string Device { get; set; }
        public string Os { get; set; }
        public string OsVersion { get; set; }
        public string Version { get; set; }
        public string PushToken { get; set; }
        public int TimeStamp { get; set; }
    }
}
