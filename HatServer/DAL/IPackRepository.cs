using System.Threading.Tasks;
using HatServer.Models;

namespace HatServer.DAL
{
    public interface IPackRepository : IRepository<Pack>
    {
        Task<Pack> GetByNameAsync(string name);
    }
}