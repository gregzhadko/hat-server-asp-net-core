using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    public class Track
    {
        [Key]
        public int Id { get; set; }

        public List<PhraseItem> PhraseItems { get; set; }
    }
}