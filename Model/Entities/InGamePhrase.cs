using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    public class InGamePhrase
    {
        public int Id { get; set; }

        public int InGameId { get; set; }
        
        public string Word { get; set; }

        public bool BadItalic { get; set; }

        public int PackId { get; set; }

        public int GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; }
    }
}