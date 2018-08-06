using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Entities;

namespace HatServer.DAL.Interfaces
{
    public interface IPackRepository : IRepository<Pack>
    {
        Task<Pack> GetByNameAsync(string name);
        Task<Pack> GetFullInfoAsync(int id);
        Task<List<Pack>> GetAllWithPhrases();
    }
}