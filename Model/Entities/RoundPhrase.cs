using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    public class RoundPhrase
    {
        public int Id { get; set; }
        public int Time { get; set; }

        public RoundPhraseState State { get; set; }

        [ForeignKey("PhraseId")]
        public GamePhrase PhraseItem { get; set; }

        public int PhraseId { get; set; }

        [ForeignKey("RoundId")]
        public Round Round { get; set; }

        public int RoundId { get; set; }
    }
}
