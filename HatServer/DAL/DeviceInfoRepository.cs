using System.Linq;
using HatServer.Data;
using HatServer.DAL.Interfaces;
using JetBrains.Annotations;
using Model.Entities;

namespace HatServer.DAL
{
    public class DeviceInfoRepository : Repository<DeviceInfo>, IDeviceInfoRepository
    {
        public DeviceInfoRepository([NotNull] GameDbContext context) : base(context)
        {
        }
    }
}