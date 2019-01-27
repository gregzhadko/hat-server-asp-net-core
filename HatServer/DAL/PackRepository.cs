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

        public async Task<Pack> GetByNameAsync(string name)
        {
            return await Entities.FirstOrDefaultAsync(p => p.Name == name);
        }

        public override async Task<Pack> GetAsync(int id)
        {
            return await Entities.Include(p => p.Phrases).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pack> GetFullInfoAsync(int id)
        {
            return await Entities.Include(p => p.Phrases).ThenInclude(p => p.ReviewStates).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Pack>> GetAllWithPhrasesAsync()
        {
            return await Entities.Include(p => p.Phrases).ThenInclude(p => p.ReviewStates).ToListAsync();
        }
    }
}
