using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    public class Stage
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Time { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }

        public int GameId { get; set; }

        public List<Round> Rounds { get; set; }
    }
}
