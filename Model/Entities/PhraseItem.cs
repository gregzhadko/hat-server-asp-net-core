using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
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

        [ForeignKey(nameof(PackId))]
        public Pack Pack { get; set; }

        public List<ReviewState> ReviewStates { get; set; } = new List<ReviewState>();

        [Required]
        public int TrackId { get; set; }

        [Required]
        public string CreatedById { get; set; }

        [ForeignKey(nameof(CreatedById))]
        public ServerUser CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [CanBeNull]
        public string ClosedById { get; set; }

        [CanBeNull]
        [ForeignKey(nameof(ClosedById))]
        public string ClosedBy { get; set; }

        public DateTime? ClosedDate { get; set; }

        [NotNull]
        public PhraseItem FormatPhrase()
        {
            Phrase = Phrase.FormatPhrase();
            Description = Description.FormatDescription();
            return this;
        }

        [NotNull]
        public PhraseItem Clone()
        {
            return new PhraseItem
            {
                TrackId = TrackId,
                Complexity = Complexity,
                Description = Description,
                PackId = PackId,
                Version = Version,
                Phrase = Phrase,
                CreatedById = CreatedById,
                CreatedDate = CreatedDate,
                ReviewStates = ReviewStates.Select(r => r.Clone()).ToList()
            };
        }
    }
}