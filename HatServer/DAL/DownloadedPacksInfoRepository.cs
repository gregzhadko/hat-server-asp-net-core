using HatServer.Data;
using JetBrains.Annotations;

namespace HatServer.DAL
{
    public class DownloadedPacksInfoRepository : Repository<DownloadedPacksInfoRepository>
    {
        protected DownloadedPacksInfoRepository([NotNull] FillerDbContext context) : base(context)
        {
        }
    }
}