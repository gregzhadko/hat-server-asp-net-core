using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Entities;

namespace HatServer.DAL.Interfaces
{
    public interface IDeviceInfoRepository : IRepository<DeviceInfo>
    {
        Task<List<DeviceInfo>> GetDistinctDevicesInfosExpectedTestsAsync();
    }
}