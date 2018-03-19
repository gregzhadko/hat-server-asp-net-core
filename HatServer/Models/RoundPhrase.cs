using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HatServer.Models
{
    public class RoundPhrase
    {
        public int Id { get; set; }
        public int Time { get; set; }

        public RoundPhraseState State { get; set; }

        [ForeignKey("PhraseId")]
        public PhraseItem PhraseItem { get; set; }
        public int PhraseId { get; set; }

        [ForeignKey("RoundId")]
        public Round Round { get; set; }
        public int RoundId { get; set; }
    }
}
