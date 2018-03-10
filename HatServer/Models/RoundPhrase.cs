using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatServer.Models
{
    public class RoundPhrase
    {
        public int Id { get; set; }
        public int Time { get; set; }

        public RoundPhraseState State { get; set; }

        public int PhraseId { get; set; }
        public PhraseItem PhraseItem { get; set; }

        public int RoundId { get; set; }
        public Round Round { get; set; }
    }
}
