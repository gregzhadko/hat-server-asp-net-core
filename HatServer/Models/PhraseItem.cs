using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HatServer.Models
{
    public class PhraseItem
    {
        public int Id { get; set; }

        [Required]
        public string Phrase { get; set; }

        [Range(1, 5)]
        public int Complexity { get; set; }

        public string Description { get; set; }

        public Pack Pack { get; set; }

        [Required]
        public int PackId { get; set; }

        public List<PhraseState> PhraseStates { get; set; }
    }
}
