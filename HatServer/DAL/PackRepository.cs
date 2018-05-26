using System.Linq;
using System.Threading.Tasks;
using HatServer.Data;
using HatServer.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace HatServer.DAL
{
    internal sealed class PackRepository : Repository<Pack>, IPackRepository
    {
        public PackRepository([NotNull] ApplicationDbContext context) : base(context)
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
        }
        
    }
}
