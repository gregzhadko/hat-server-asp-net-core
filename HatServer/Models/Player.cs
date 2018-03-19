using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HatServer.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("TeamId")]
        public Team Team { get; set; }

        public int TeamId { get; set; }

        public List<Round> Rounds { get; set; }
    }
}
