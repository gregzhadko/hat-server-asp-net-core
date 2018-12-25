using System.Collections.Generic;
using System.Threading.Tasks;

namespace HatServer.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<T> GetAsync(int id);
        Task InsertAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
