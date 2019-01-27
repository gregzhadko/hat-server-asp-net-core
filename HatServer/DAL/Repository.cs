using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public virtual async Task<List<T>> GetAllAsync() => await Entities.ToListAsync();

        public virtual async Task<T> GetAsync(int id) => await Entities.FindAsync(id);

        public virtual async Task InsertAsync([NotNull] T entity)
        {
            Entities.Add(entity); // We don't need to call AddAsync, because there is no I/O operations.
            await Context.SaveChangesAsync();
        }


        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}