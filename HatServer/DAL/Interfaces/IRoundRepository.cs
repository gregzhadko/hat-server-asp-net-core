using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Entities;

namespace HatServer.DAL.Interfaces
{
    public interface IRoundRepository : IRepository<Round>
    {
        Task<List<Round>> GetNotAttachedRoundsByGameGuidAsync(string gameInGameId);
    }
}