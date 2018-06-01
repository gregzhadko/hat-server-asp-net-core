using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Utilities;

namespace Model.Entities
{
    [DebuggerDisplay("{Phrase}, {Complexity}, {Description}")]
    public sealed class PhraseItem
    {
        public int Id { get; set; }

        [ConcurrencyCheck]
        public int Version { get; set; }

        [Required]
        public string Phrase { get; set; }

        [Range(1, 5)]
        public double? Complexity { get; set; }

        public string Description { get; set; }

        [Required]
        public int PackId { get; set; }

        [ForeignKey("PackId")]
        public Pack Pack { get; set; }

        public List<ReviewState> ReviewStates { get; set; } = new List<ReviewState>();

        public PhraseItem FormatPhrase()
        {
            Phrase = Phrase.FormatPhrase();
            Description = Description.FormatDescription();
            return this;
        }
    }
}
