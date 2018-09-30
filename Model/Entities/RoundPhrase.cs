using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    public class RoundPhrase
    {
        public int Id { get; set; }
        
        public RoundPhraseStateEnum State { get; set; }

        public int WordId { get; set; }
        
        public int Time { get; set; }

        [ForeignKey("RoundId")]
        public Round Round { get; set; }

        public int RoundId { get; set; }
    }
}
