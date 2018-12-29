using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Utilities;

namespace Model.Entities
{
    public class DeviceInfo
    {
        [UsedImplicitly]
        public DeviceInfo(){}

        [Key]
        public int Id { get; set; }
        public Guid DeviceGuid { get; set; }
        public string DeviceModel { get; set; }
        public string Device { get; set; }
        public string OsName { get; set; }
        public string OsVersion { get; set; }
        public string Version { get; set; }
        public string PushToken { get; set; }
        public int TimeStamp { get; set; }
        public DateTime DateTime => TimeStamp.ToDateTime();
    }
}    