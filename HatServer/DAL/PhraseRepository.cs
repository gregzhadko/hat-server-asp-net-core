using System;
using HatServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Model;

namespace HatServer.DAL
{
    public interface IPhraseRepository : IRepository<PhraseItem>
    {
        Task<PhraseItem> GetByNameAsync(string phrase);
    }

    internal sealed class PhraseRepository : Repository<PhraseItem>, IPhraseRepository
    {
        public PhraseRepository([NotNull] ApplicationDbContext context) : base(context)
        {
        }

        public override IEnumerable<PhraseItem> GetAll()
        {
            return Context.PhraseItems.Include(p => p.ReviewStates);
        }

        public override Task<PhraseItem> GetAsync(int id)
        {
            return Context.PhraseItems.Include(p => p.ReviewStates).FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<PhraseItem> GetByNameAsync(string phrase)
        {
            return Context.PhraseItems.Include(p => p.ReviewStates).FirstOrDefaultAsync(p => string.CompareOrdinal(p.Phrase, phrase) == 0);
        }
    }
}
