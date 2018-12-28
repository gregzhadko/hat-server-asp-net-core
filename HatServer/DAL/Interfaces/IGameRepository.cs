using System.Collections.Generic;
using System.Linq;
using Model;
using Model.Entities;

namespace HatServer.DAL.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Game GetGameByGuid(string roundGameGUID);
        List<FullGame> GetFullGames();
    }
}