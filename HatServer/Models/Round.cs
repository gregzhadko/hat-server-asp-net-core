using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatServer.Models
{
    public class Round
    {
        public int Id { get; set; }
        public int Timestamp { get; set; }
        public int RoundNumber { get; set; }
        public int Time { get; set; }

        public int SettingsId { get; set; }
        public Settings Settings { get; set; }

        public List<RoundPhrase> RoundPhrases { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int StageId { get; set; }
        public Stage Stage { get; set; }
    }
}
