using System.Collections.Generic;
using System.Diagnostics;

namespace HatServer.Models
{
    [DebuggerDisplay("{Id}, {Name}, {Description}")]
    public class Pack
    {
        public int Id { get; set; }
        public string Language { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IList<PhraseItem> Phrases { get; set; } = new List<PhraseItem>();

        public override string ToString() => $"{Id}. {Name}\t{Description}";
    }
}
