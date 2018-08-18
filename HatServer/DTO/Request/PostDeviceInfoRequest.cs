using System;

namespace HatServer.DTO.Request
{
    public class PostDeviceInfoRequest
    {
        public Guid DeviceId { get; set; }
        public string DeviceModel { get; set; }
        public string Device { get; set; }
        public string OsName { get; set; }
        public string OsVersion { get; set; }
        public string Version { get; set; }
        public string PushToken { get; set; }
        public int TimeStamp { get; set; }
    }
}