using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace HatServer.Models
{
    [DebuggerDisplay("{Id}, {Name}, {Description}")]
    public class Pack
    {
        public int Id { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public IList<PhraseItem> Phrases { get; set; } = new List<PhraseItem>();

        public override string ToString() => $"{Id}. {Name}\t{Description}";
    }
}
