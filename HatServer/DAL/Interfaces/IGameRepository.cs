using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Model.Entities;

namespace HatServer.DAL.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Task AssignDeviceToUnassignedGamesAsync([NotNull]DeviceInfo device);
    }
}