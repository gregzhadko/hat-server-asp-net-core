using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using Model.Entities;

namespace HatServer.DAL.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Game GetGameByGuid(string roundGameGUID);
        IEnumerable<FullGame> GetFullGames();
        Task<FullGame> GetFullGameAsync(int id);
    }
}