using System;
using JetBrains.Annotations;

namespace Model.Entities
{
    public class DeviceInfo
    {
        [UsedImplicitly]
        public DeviceInfo(){}

        public int Id { get; set; }
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