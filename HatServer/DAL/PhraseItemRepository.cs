using HatServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Model;

namespace HatServer.DAL
{
    internal sealed class PhraseItemRepository : Repository<PhraseItem>
    {
        public PhraseItemRepository([NotNull] ApplicationDbContext context) : base(context)
        {
        }

        public override IEnumerable<PhraseItem> GetAll()
        {
            return Context.PhraseItems.Include(p => p.Pack);
        }

        public override Task<PhraseItem> GetAsync(int id)
        {
            return Context.PhraseItems.Include(p => p.Pack).SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}
