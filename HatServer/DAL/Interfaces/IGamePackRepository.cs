using System.Threading.Tasks;
using Model.Entities;

namespace HatServer.DAL.Interfaces
{
    public interface IGamePackRepository : IRepository<GamePack>
    {
        Task<GamePackIcon> GetPackIcon(int packId);
    }
}