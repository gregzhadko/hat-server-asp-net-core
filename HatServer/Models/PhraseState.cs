using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace HatServer.Models
{
    public class PhraseState
    {
        public int Id { get; set; }

        [ForeignKey("PhraseItemId")]
        public virtual PhraseItem PhraseItem { get; set; }
        public int PhraseItemId { get; set; }

        public string UserName { get; set; }

        public ReviewState ReviewState { get; set; }

        [NotNull]
        public override string ToString() => $"{UserName}: {ReviewState}";
    }
}
