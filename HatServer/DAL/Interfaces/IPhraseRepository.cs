using System.Threading.Tasks;
using Model;

namespace HatServer.DAL
{
    public interface IPhraseRepository : IRepository<PhraseItem>
    {
        Task<PhraseItem> GetByNameAsync(string phrase);
    }
}