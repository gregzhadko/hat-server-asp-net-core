﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string InGameId { get; set; } //deviceid_timestamp

        public Guid DeviceInfoGuid { get; set; }

        public int? DeviceInfoId { get; set; }
        
        [ForeignKey("DeviceInfoId")]
        public DeviceInfo DeviceInfo { get; set; }
        
        public DateTime StartDate { get; set; }

        public List<Team> Teams { get; set; }
        public List<Stage> Stages { get; set; }
    }
}