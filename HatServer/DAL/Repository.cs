using HatServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatServer.DAL.Interfaces;
using JetBrains.Annotations;

namespace HatServer.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        protected readonly DbSet<T> Entities;

        // ReSharper disable once MemberCanBeProtected.Global
        public Repository([NotNull] DbContext context)
        {
            Context = context;
            Entities = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll() => Entities.AsEnumerable();

        public virtual Task<T> GetAsync(int id) => Entities.FindAsync(id);

        public virtual async Task InsertAsync([NotNull] T entity)
        {
            await Entities.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task InsertRangeAsync([NotNull] IEnumerable<T> entities)
        {
            await Entities.AddRangeAsync(entities);
            await Context.SaveChangesAsync();
        }

        public virtual Task UpdateAsync([NotNull] T entity)
        {
            return Context.SaveChangesAsync();
        }

        public virtual Task DeleteAsync([CanBeNull] T entity)
        {
            if (entity == null)
            {
                return Task.CompletedTask;
            }

            Entities.Remove(entity);
            return Context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await Entities.FindAsync(id);
            await DeleteAsync(entity);
        }
    }
}