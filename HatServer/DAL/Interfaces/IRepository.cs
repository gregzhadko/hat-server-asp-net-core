using System.Collections.Generic;
using System.Threading.Tasks;

namespace HatServer.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task InsertAsync(T entity);
        Task SaveChangesAsync();
    }
}
