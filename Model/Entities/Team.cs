using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }

        public int GameId { get; set; }

        public List<Player> Players { get; set; }
    }
}
