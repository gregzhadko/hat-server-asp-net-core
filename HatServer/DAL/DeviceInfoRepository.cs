using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatServer.Data;
using HatServer.DAL.Interfaces;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using MoreLinq;

namespace HatServer.DAL
{
    public class DeviceInfoRepository : Repository<DeviceInfo>, IDeviceInfoRepository
    {
        public DeviceInfoRepository([NotNull] GameDbContext context) : base(context)
        {
        }

        public async Task<List<DeviceInfo>> GetDistinctDevicesInfosExpectedTestsAsync()
        {
            var list = await Entities.Where(i =>  i.DeviceModel != "x86_64").OrderBy(d => d.TimeStamp).ToListAsync();
            return list.DistinctBy(d => d.DeviceGuid).OrderByDescending(d => d.TimeStamp).ToList();
        } 
    }
}