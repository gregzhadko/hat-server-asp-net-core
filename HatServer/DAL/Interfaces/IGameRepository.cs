using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using Model.Entities;

namespace HatServer.DAL.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<Game> GetGameByGuidAsync(string roundGameGUID);
        Task<List<FullGame>> GetFullGamesAsync();
        Task<FullGame> GetFullGameAsync(int id);
    }
}