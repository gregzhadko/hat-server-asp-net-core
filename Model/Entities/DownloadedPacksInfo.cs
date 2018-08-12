using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    public class DownloadedPacksInfo
    {
        [Required]
        public Guid GadgetId { get; set; }
        
        [ForeignKey(nameof(GamePackId))]
        public GamePack GamePack { get; set; }
        
        [Required]
        public int GamePackId { get; set; }
        
        [Required]
        public DateTime DownloadedTime { get; set; }
    }
}