using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    public class Round
    {
        public int Id { get; set; }
        public int Timestamp { get; set; }
        public int RoundNumber { get; set; }
        public int Time { get; set; }

        [ForeignKey("SettingsId")]
        public Settings Settings { get; set; }

        public int SettingsId { get; set; }

        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        public int? PlayerId { get; set; }

        //public Stage Stage { get; set; }

        //public int? StageId { get; set; }

        public List<RoundPhrase> RoundPhrases { get; set; }
    }
}
