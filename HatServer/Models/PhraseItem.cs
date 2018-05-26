using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;
using Utilities;

namespace HatServer.Models
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
        public int Complexity { get; set; }

        public string Description { get; set; }

        [Required]
        public int PackId { get; set; }

        [ForeignKey("PackId")]
        public Pack Pack { get; set; }

        public List<PhraseState> PhraseStates { get; set; } = new List<PhraseState>();

        [CanBeNull]
        public string Author => PhraseStates.FirstOrDefault(s => s.ReviewState == ReviewState.Accept)?.UserName;

        public void FormatPhrase()
        {
            Phrase = Phrase.FormatPhrase();
            Description = Description.FormatDescription();
        }

        //TODO: remove this when use a new server
        [NotNull]
        public PhraseItem FluentClone()
        {
            return new PhraseItem
            {
                Phrase = Phrase,
                Description = Description,
                Complexity = Complexity,
                PhraseStates = new List<PhraseState>
                {
                    new PhraseState {ReviewState = ReviewState.Accept, UserName = Constants.DefaultUserName}
                }
            };
        }
    }
}
