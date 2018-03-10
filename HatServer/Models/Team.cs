using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatServer.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public List<Player> Players { get; set; }
    }
}
