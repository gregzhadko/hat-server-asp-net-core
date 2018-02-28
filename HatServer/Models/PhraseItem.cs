using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Newtonsoft.Json;

namespace HatServer.Models
{
    [DebuggerDisplay("{Phrase}, {Complexity}, {Description}")]
    public class PhraseItem
    {
        public int Id { get; set; }

        [Required]
        public string Phrase { get; set; }

        [Range(1, 5)]
        public int Complexity { get; set; }

        public string Description { get; set; }

        [Required]
        public int PackId { get; set; }

        public virtual Pack Pack { get; set; }

        public List<PhraseState> PhraseStates { get; set; } = new List<PhraseState>();

        [NotMapped]
        public Dictionary<string, int> Reviews
        {
            set
            {
                if (value == null || value.Count == 0)
                {
                    return;
                }

                foreach (var authorState in value)
                {
                    new PhraseState() { PhraseItemId = Id, State = (State)authorState.Value };
                }
            }
        }
    }

}
