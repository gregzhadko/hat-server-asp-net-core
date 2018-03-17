namespace HatServer.Models
{
    public class PhraseState
    {
        public int Id { get; set; }
        public int PhraseItemId { get; set; }
        public virtual PhraseItem PhraseItem { get; set; }
        public virtual ServerUser ServerUser { get; set; }
        public int ApplicationUserId { get; set; }
        public ReviewState ReviewState { get; set; }
    }
}
