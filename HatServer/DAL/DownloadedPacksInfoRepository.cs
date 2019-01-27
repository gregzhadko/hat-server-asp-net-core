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

        public async Task<List<DownloadedPacksInfo>> GetDailyDownloadsForPackAsync(int packId)
        {
            return await Entities.Where(d => d.GamePackId == packId && d.DownloadedTime.Date == DateTime.Today).ToListAsync();
        }

        public async Task<List<DownloadedPacksInfo>> GetDownloadsForLastHoursAsync(int hoursNumber)
        {
            return await Entities
                .Where(d => d.DownloadedTime.Date > DateTime.Now.AddHours(-hoursNumber))
                .Include(d => d.GamePack)
                .ToListAsync();
        }

        public override async Task<List<DownloadedPacksInfo>> GetAllAsync()
        {
            return await Entities.Include(d => d.GamePack).ToListAsync();
        }

        public override async Task<DownloadedPacksInfo> GetAsync(int id)
        {
            return await Entities.Include(d => d.GamePack).FirstOrDefaultAsync();
        }
    }
}