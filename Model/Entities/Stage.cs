using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace Model.Entities
{
    [UsedImplicitly]
    public sealed class Stage
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Time { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }

        public int GameId { get; set; }

        public List<Round> Rounds { get; set; }
    }
}
