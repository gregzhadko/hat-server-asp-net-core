using System.Threading.Tasks;
using HatServer.DTO.Request;
using Model.Entities;

namespace HatServer.DAL.Interfaces
{
    public interface IPhraseRepository : IRepository<PhraseItem>
    {
        Task<PhraseItem> GetByNameAsync(string phrase);
        Task<int> GetMaxTrackIdAsync();
        Task<PhraseItem> GetLatestByTrackIdAsync(int trackId);
        Task<PhraseItem> GetByNameExceptTrackIdAsync(string phrase, int trackId);
        Task DeleteAsync(PhraseItem phrase, string userId);
        Task<PhraseItem> AddReviewAsync(PhraseItem phrase, ServerUser user, PostReviewRequest request);
        Task<PhraseItem> UpdatePhraseAsync(PutPhraseItemRequest request, ServerUser user, PhraseItem existingPhrase);
    }
}