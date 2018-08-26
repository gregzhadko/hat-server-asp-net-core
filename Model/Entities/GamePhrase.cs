using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace Model.Entities
{
    public class GamePhrase
    {
        [UsedImplicitly]
        public GamePhrase()
        {
        }

        public GamePhrase([NotNull] PhraseItem phraseItem, [NotNull] Pack pack)
        {
            Phrase = phraseItem.Phrase;
            Complexity = phraseItem.Complexity;
            Description = phraseItem.Description;
            GamePackId = pack.Id;
        }

        public int Id { get; set; }

        [Required]
        public string Phrase { get; set; }

        [Range(1, 5)]
        public double? Complexity { get; set; }

        public string Description { get; set; }

        [Required]
        public int GamePackId { get; set; }

        [ForeignKey(nameof(GamePackId))]
        public GamePack GamePack { get; set; }
    }
}