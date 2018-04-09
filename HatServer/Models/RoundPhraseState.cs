using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HatServer.Extensions;

namespace HatServer.Models
{
    public enum RoundPhraseStateEnum
    {
        [Description("Guessed")]
        Guessed,

        [Description("Skipped")]
        Skipped,

        [Description("Deleted")]
        Deleted
    }

    public class RoundPhraseState
    {
        private RoundPhraseState(RoundPhraseStateEnum @enum)
        {
            Id = (int)@enum;
            Name = @enum.ToString();
            Description = @enum.GetEnumDescription();
        }

        protected RoundPhraseState() { } //For EF

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public static implicit operator RoundPhraseState(RoundPhraseStateEnum @enum) => new RoundPhraseState(@enum);

        public static implicit operator RoundPhraseStateEnum(RoundPhraseState faculty) => (RoundPhraseStateEnum)faculty.Id;
    }
}
