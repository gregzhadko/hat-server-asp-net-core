using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatServer.Models
{
    public class Stage
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Time { get; set; }


        public int GameId { get; set; }
        public Game Game { get; set; }

        public List<Round> Rounds { get; set; }
    }
}
