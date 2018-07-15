using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using FluentValidation;
using JetBrains.Annotations;

namespace Model.Entities
{
    [DebuggerDisplay("{Id}, {Name}, {Description}")]
    public sealed class ProdPack
    {
        public int Id { get; set; }

        public int Version { get; set; }

        public bool Free {get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Name { get; set; }

        [CanBeNull]
        public string Description { get; set; }

        public IList<ProdPhraseItem> Phrases { get; set; } = new List<ProdPhraseItem>();

        [NotNull]
        public override string ToString() => $"{Id}. {Name}\t{Description}";
    }
}
