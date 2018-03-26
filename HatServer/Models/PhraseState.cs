using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HatServer.Models
{
    public class PhraseState
    {
        public int Id { get; set; }

        [ForeignKey("PhraseItemId")]
        public virtual PhraseItem PhraseItem { get; set; }
        public int PhraseItemId { get; set; }

        [ForeignKey("ServerUserId")]
        public ServerUser ServerUser { get; set; }

        public ReviewState ReviewState { get; set; }

        public override string ToString() => $"{ServerUser.UserName}: {ReviewState}";
    }
}
