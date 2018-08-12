using HatServer.Data;
using HatServer.DAL.Interfaces;
using JetBrains.Annotations;
using Model.Entities;

namespace HatServer.DAL
{
    public class DownloadedPacksInfoRepository : Repository<DownloadedPacksInfo>, IDownloadedPacksInfoRepository
    {
        public DownloadedPacksInfoRepository([NotNull] GameDbContext context) : base(context)
        {
        }
    }
}