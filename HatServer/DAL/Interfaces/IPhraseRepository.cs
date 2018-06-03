using System.Threading.Tasks;
using JetBrains.Annotations;
using Model.Entities;

namespace HatServer.DAL.Interfaces
{
    public interface IPhraseRepository : IRepository<PhraseItem>
    {
        Task<PhraseItem> GetByNameAsync(string phrase);
        Task<int> GetMaxTrackIdAsync();
        Task<PhraseItem> GetLatestByTrackId(int trackId);
        Task CloseAndInsert([NotNull] PhraseItem newPhrase, [NotNull] PhraseItem oldPhrase, string userId);
        Task<PhraseItem> GetByNameExceptTrackIdAsync(string phrase, int trackId);
    }
}