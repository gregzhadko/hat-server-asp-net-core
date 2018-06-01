using System.Threading.Tasks;
using Model;
using Model.Entities;

namespace HatServer.DAL
{
    public interface IPackRepository : IRepository<Pack>
    {
        Task<Pack> GetByNameAsync(string name);
        Task<Pack> GetFullInfo(int id);
    }
}