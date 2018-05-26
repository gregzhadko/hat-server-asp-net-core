using HatServer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace HatServer.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<T> Entities;

        public Repository([NotNull] ApplicationDbContext context)
        {
            Context = context;
            Entities = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Entities.AsEnumerable();
        }

        public virtual Task<T> GetAsync(int id)
        {
            return Entities.FindAsync(id);
        }

        public virtual async Task InsertAsync([NotNull] T entity)
        {
            await Entities.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public virtual Task UpdateAsync([NotNull] T entity)
        {
            return Context.SaveChangesAsync();
        }

        public virtual Task DeleteAsync([NotNull] T entity)
        {
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
