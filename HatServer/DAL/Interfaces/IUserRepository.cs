using System.Threading.Tasks;
using Model.Entities;

namespace HatServer.DAL.Interfaces
{
    public interface IUserRepository : IRepository<ServerUser>
    {
        Task<ServerUser> GetByNameAsync(string name);
    }
}
