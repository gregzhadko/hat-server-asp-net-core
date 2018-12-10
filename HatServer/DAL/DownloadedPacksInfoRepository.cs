using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatServer.Data;
using HatServer.DAL.Interfaces;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace HatServer.DAL
{
    public sealed class DownloadedPacksInfoRepository : Repository<DownloadedPacksInfo>, IDownloadedPacksInfoRepository
    {
        public DownloadedPacksInfoRepository([NotNull] GameDbContext context) : base(context)
        {
        }

        public Task<List<DownloadedPacksInfo>> GetDailyDownloadsForPack(int packId)
        {
            return Entities.Where(d => d.GamePackId == packId && d.DownloadedTime.Date == DateTime.Today).ToListAsync();
        }

        public Task<List<DownloadedPacksInfo>> GetDownloadsForLastHoursAsync(int hoursNumber)
        {
            return Entities
                .Where(d => d.DownloadedTime.Date > DateTime.Now.AddHours(-hoursNumber))
                .Include(d => d.GamePack)
                .ToListAsync();
        }

        public override IEnumerable<DownloadedPacksInfo> GetAll()
        {
            return Entities.Include(d => d.GamePack);
        }

        public override Task<DownloadedPacksInfo> GetAsync(int id)
        {
            return Entities.Include(d => d.GamePack).FirstOrDefaultAsync();
        }
    }
}