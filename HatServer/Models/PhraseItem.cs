using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HatServer.Models
{
    public class PhraseItem
    {
        public int Id { get; set; }

        [Required]
        public string Phrase { get; set; }

        [Range(1, 5)]
        public int Complexity { get; set; }

        public string Description { get; set; }

        public Pack Pack { get; set; }

        public int PackId { get; set; }
    }
}
