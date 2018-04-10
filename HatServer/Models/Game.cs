using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HatServer.Models
{
    public class Game
    {
        public int Id { get; set; }

        public List<Team> Teams { get; set; }
        public List<Stage> Stages { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }

    }
}
