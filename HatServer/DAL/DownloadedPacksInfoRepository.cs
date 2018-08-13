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
    public class DownloadedPacksInfoRepository : Repository<DownloadedPacksInfo>, IDownloadedPacksInfoRepository
    {
        public DownloadedPacksInfoRepository([NotNull] GameDbContext context) : base(context)
        {
        }

        public Task<List<DownloadedPacksInfo>> GetDailyDownloadsForPack(int packId)
        {
            return Entities.Where(d => d.GamePackId == packId && d.DownloadedTime.Date == DateTime.Today).ToListAsync();
        }
    }
}