using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
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
