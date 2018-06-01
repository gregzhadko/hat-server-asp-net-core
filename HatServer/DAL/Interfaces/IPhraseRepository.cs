using System.Threading.Tasks;
using Model;
using Model.Entities;

namespace HatServer.DAL
{
    public interface IPhraseRepository : IRepository<PhraseItem>
    {
        Task<PhraseItem> GetByNameAsync(string phrase);
    }
}