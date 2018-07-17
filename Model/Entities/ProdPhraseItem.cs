using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;
using Utilities;

namespace Model.Entities
{
    [DebuggerDisplay("{Phrase}, {Complexity}, {Description}")]
    public sealed class ProdPhraseItem
    {
        public ProdPhraseItem()
        {
        }

        public ProdPhraseItem([NotNull] PhraseItem phraseItem, [NotNull] Pack pack)
        {
            Phrase = phraseItem.Phrase;
            Complexity = phraseItem.Complexity;
            Description = phraseItem.Description;
            ProdPackId = pack.Id;
        }

        public int Id { get; set; }

        [Required]
        public string Phrase { get; set; }

        [Range(1, 5)]
        public double? Complexity { get; set; }

        public string Description { get; set; }

        [Required]
        public int ProdPackId { get; set; }

        [ForeignKey(nameof(ProdPackId))]
        public ProdPack ProdPack { get; set; }
    }
}