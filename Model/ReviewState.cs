using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace Model
{
    public sealed class ReviewState
    {
        public int Id { get; set; }

        [ForeignKey("PhraseItemId")]
        public PhraseItem PhraseItem { get; set; }

        public int PhraseItemId { get; set; }

        public string UserName { get; set; }

        public State State { get; set; }

        public string Comment { get; set; }
        public bool ClearReviews { get; set; }

        [NotNull]
        public override string ToString() => $"{UserName}: {State}";
    }
}
