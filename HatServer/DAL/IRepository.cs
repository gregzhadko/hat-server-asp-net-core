using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HatServer.DAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void InsertAsync(TEntity item);
        Task<TEntity> GetByIDAsync(object id);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        void DeleteAsync(object id);
        void Delete(TEntity item);
        void Update(TEntity item);
        IQueryable<TEntity> GetWithRawSql(string query, params object[] parameters);
    }
}
