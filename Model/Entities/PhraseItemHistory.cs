using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Model
{
    [DebuggerDisplay("{Phrase}, {Complexity}, {Description}")]
    public sealed class PhraseItemHistory
    {
        public int Id { get; set; }

        public int Version { get; set; }

        [Required]
        public string Phrase { get; set; }

        [ForeignKey("PhraseItemId")]
        public PhraseItem PhraseItem { get; set; }

        public int PhraseItemId { get; set; }

        [Range(1, 5)]
        public int Complexity { get; set; }

        public string Description { get; set; }

        public List<ReviewState> ReviewStates { get; set; } = new List<ReviewState>();
    }
}
