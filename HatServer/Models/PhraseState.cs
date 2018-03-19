using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HatServer.Models
{
    public class PhraseState
    {
        public int Id { get; set; }
        //public int PhraseItemId { get; set; }

        //public virtual PhraseItem PhraseItem { get; set; }

        [ForeignKey("ServerUserId")]
        public ServerUser ServerUser { get; set; }
        //public int ServerUserId { get; set; }

        public ReviewState ReviewState { get; set; }
    }
}
