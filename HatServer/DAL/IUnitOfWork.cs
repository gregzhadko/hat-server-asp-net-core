using HatServer.Models;

namespace HatServer.DAL
{
    public interface IUnitOfWork
    {
        IRepository<Pack> PackRepository { get; }
        void Save();
    }
}
