using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;

namespace Model.Entities
{
    [DebuggerDisplay("{Id}, {Name}, {Description}")]
    public class GamePack
    {
        [UsedImplicitly]
        public GamePack()
        {
        }

        public GamePack([NotNull] Pack pack)
        {
            Id = pack.Id;
            Paid = pack.Paid;
            Language = pack.Language;
            Name = pack.Name;
            Description = pack.Description;
            Version = pack.Version + 1;
            Phrases = pack.Phrases.Select(p => new GamePhrase(p, pack)).ToList();
        }

        public int Id { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Name { get; set; }

        [CanBeNull]
        public string Description { get; set; }

        public List<GamePhrase> Phrases { get; set; }

        public int Version { get; set; }

        public bool Paid { get; set; }

        public int Count { get; set; }

        public byte[] Icon { get; set; }

        [NotNull]
        public override string ToString() => $"{Id}. {Name}\t{Description}";
    }
}