using System;
using JetBrains.Annotations;

namespace HatServer.DTO.Request
{
    public class PostDeviceInfoRequest
    {
        public Guid DeviceId { get; set; }
        [UsedImplicitly] public string DeviceModel { get; set; }
        [UsedImplicitly] public string Device { get; set; }
        [UsedImplicitly] public string OsName { get; set; }
        [UsedImplicitly] public string OsVersion { get; set; }
        [UsedImplicitly] public string Version { get; set; }
        [UsedImplicitly] public string PushToken { get; set; }
        [UsedImplicitly] public int TimeStamp { get; set; }
    }
}