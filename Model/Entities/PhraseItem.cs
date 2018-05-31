using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using FluentValidation;
using JetBrains.Annotations;
using Utilities;

namespace Model
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

        public void FormatPhrase()
        {
            Phrase = Phrase.FormatPhrase();
            Description = Description.FormatDescription();
        }
    }

    public class PhraseValidator : AbstractValidator<PhraseItem>
    {
        public PhraseValidator()
        {
            RuleFor(p => p.Phrase).NotEmpty();
            RuleFor(p => p.Complexity).InclusiveBetween(1, 5).When(p => p.Complexity != null);
            RuleFor(p => p.PackId).NotEmpty();
        }
    }
}
