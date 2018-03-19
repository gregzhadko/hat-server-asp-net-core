using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HatServer.Models
{
    public class Stage
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Time { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }
        public int GameId { get; set; }

        //public List<Round> Rounds { get; set; }
    }
}
