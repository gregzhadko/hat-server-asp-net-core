using System;
using System.Collections.Generic;

namespace Model.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string InGameId { get; set; } //deviceid_timestamp

        public Guid DeviceId { get; set; }
        public DateTime StartDate { get; set; }

        public List<Team> Teams { get; set; }
        public List<Stage> Stages { get; set; }
    }
}