using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatServer.Models
{
    public class PhraseState
    {
        public int Id { get; set; }
        public int PhraseItemId { get; set; }
        public PhraseItem PhraseItem { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int ApplicationUserId { get; set; }
        public State State { get; set; }
    }
}
