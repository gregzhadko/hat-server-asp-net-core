using HatServer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatServer.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<T> Entities;

        public Repository(ApplicationDbContext context)
        {
            Context = context;
            Entities = context.Set<T>();
        }
        public virtual IEnumerable<T> GetAll() => Entities.AsEnumerable();

        public virtual Task<T> GetAsync(int id) => Entities.FindAsync(id);

        public virtual async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await Entities.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public virtual Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return Context.SaveChangesAsync();
        }

        public virtual Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
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
