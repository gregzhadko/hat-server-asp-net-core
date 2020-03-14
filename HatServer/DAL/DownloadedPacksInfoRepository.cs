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
            var startDate = DateTime.Now.AddHours(-hoursNumber);
            return await Entities
                .Where(d => d.DownloadedTime > startDate)
                .Include(d => d.GamePack)
                .ToListAsync();
        }

        public override async Task<List<DownloadedPacksInfo>> GetAllAsync()
        {
            return await Entities.Include(d => d.GamePack).ToListAsync();
        }

        public async Task<List<DownloadedPacksInfo>> GetWithPagination(int pageNumber)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentOutOfRangeException("pageNumber should be positive");
            }

            const int rowsPerPage = 100;
            return await Entities
                .Include(d => d.GamePack)
                .OrderByDescending(d => d.DownloadedTime)
                .Skip((pageNumber - 1) * rowsPerPage)
                .Take(rowsPerPage)
                .ToListAsync();
        }

        public override async Task<DownloadedPacksInfo> GetAsync(int id)
        {
            return await Entities.Include(d => d.GamePack).FirstOrDefaultAsync();
        }
    }
}