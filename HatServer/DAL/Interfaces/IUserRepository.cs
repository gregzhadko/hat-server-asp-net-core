using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatServer.DAL.Interfaces
{
    public interface IUserRepository : IRepository<ServerUser>
    {
        Task<ServerUser> GetByNameAsync(string name);
    }
}
