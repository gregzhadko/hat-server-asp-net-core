using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using FluentValidation;
using JetBrains.Annotations;

namespace Model.Entities
{
    [DebuggerDisplay("{Id}, {Name}, {Description}")]
    public sealed class ProdPack
    {
        public ProdPack([NotNull] Pack pack)
        {
            Free = pack.Free;
            Language = pack.Language;
            Name = pack.Name;
            Description = pack.Description;
            Version = pack.Version + 1;
            Phrases = pack.Phrases.Select(p => new ProdPhraseItem(p, pack)).ToList();
        }

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