using System.Threading.Tasks;
using Model.Entities;

namespace HatServer.DAL.Interfaces
{
    public interface IPhraseRepository : IRepository<PhraseItem>
    {
        Task<PhraseItem> GetByNameAsync(string phrase);
        Task<int> GetMaxTrackIdAsync();
        Task<PhraseItem> GetLatestByTrackId(int trackId);
    }
}