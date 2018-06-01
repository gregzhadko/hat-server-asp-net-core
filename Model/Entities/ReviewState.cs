using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace Model.Entities
{
    public sealed class ReviewState
    {
        public int Id { get; set; }

        [ForeignKey("PhraseItemId")]
        public PhraseItem PhraseItem { get; set; }

        public int PhraseItemId { get; set; }

        [ForeignKey("UserId")]
        public ServerUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public State State { get; set; }

        public string Comment { get; set; }

        [NotNull]
        public override string ToString() => $"{User.UserName}: {State}";
    }
}
