using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    public class GamePackIcon
    {
        public int Id { get; set; }
        
        public byte[] Icon { get; set; }

        public int GamePackId { get; set; }

        [ForeignKey(nameof(GamePackId))]
        public GamePack GamePack { get; set; }
    }
}