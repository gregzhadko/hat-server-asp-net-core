using System.Threading.Tasks;
using HatServer.Data;
using HatServer.DAL.Interfaces;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace HatServer.DAL
{
    public class ProdPackRepository : Repository<ProdPack, StatisticsDbContext>, IProdPackRepository
    {
        public ProdPackRepository([NotNull] StatisticsDbContext context) : base(context)
        {
        }

        public async Task InsertAsync(Pack pack)
        {
            var existing = await Entities.FirstOrDefaultAsync(e => e.Name == pack.Name);
            if (existing != null)
            {
                return;
            }

            var prodPack = new ProdPack(pack);
            await base.InsertAsync(prodPack);
        }

        public async Task UpdateAsync(Pack pack)
        {
            var existing = await Entities.FirstOrDefaultAsync(e => e.Name == pack.Name);
            if (existing == null)
            {
                return;
            }

            var prodPack = new ProdPack(pack);
            await base.UpdateAsync(prodPack);
        }
    }
}