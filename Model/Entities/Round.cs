using System;
using System.Collections.Generic;

namespace Model.Entities
{
    public class Round
    {
        public int Id { get; set; }

        public string GameGUID { get; set; }
        
        public int GameId { get; set; }
        
        public int RoundNumber { get; set; }

        public int PlayerId { get; set; }

        public int Time { get; set; }
        
        public Guid DeviceId { get; set; }

        public DateTime StartTime { get; set; }

        public int Stage { get; set; }

        public List<RoundPhrase> Words { get; set; }

        public Settings Settings { get; set; }
    }

}
