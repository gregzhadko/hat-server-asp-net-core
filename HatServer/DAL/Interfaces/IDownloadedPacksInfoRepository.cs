using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Entities;

namespace HatServer.DAL.Interfaces
{
    public interface IDownloadedPacksInfoRepository : IRepository<DownloadedPacksInfo>
    {
        Task<List<DownloadedPacksInfo>> GetDailyDownloadsForPack(int packId);
        Task<List<DownloadedPacksInfo>> GetDownloadsForLastHoursAsync(int hoursNumber);
    }
}