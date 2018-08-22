using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    public class DownloadedPacksInfo
    {
        public int Id { get; set; }

        [Required]
        public Guid DeviceId { get; set; }

        [ForeignKey(nameof(GamePackId))]
        public GamePack GamePack { get; set; }

        [Required]
        public int GamePackId { get; set; }

        [Required]
        public DateTime DownloadedTime { get; set; }
    }
}