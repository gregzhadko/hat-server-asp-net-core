using System.Collections.Generic;
using System.Threading.Tasks;
using HatServer.Data;
using HatServer.DAL.Interfaces;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace HatServer.DAL
{
    public sealed class PackRepository : Repository<Pack>, IPackRepository
    {
        public PackRepository([NotNull] FillerDbContext context) : base(context)
        {
        }

        public Task<Pack> GetByNameAsync(string name)
        {
            return Entities.FirstOrDefaultAsync(p => p.Name == name);
        }

        public override Task<Pack> GetAsync(int id)
        {
            return Entities.Include(p => p.Phrases).FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<Pack> GetFullInfoAsync(int id)
        {
            return Entities.Include(p => p.Phrases).ThenInclude(p => p.ReviewStates).FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<List<Pack>> GetAllWithPhrases()
        {
            var packs = Entities.Include(p => p.Phrases).ThenInclude(p => p.ReviewStates).ToListAsync();
            return packs;
        }
    }
}
