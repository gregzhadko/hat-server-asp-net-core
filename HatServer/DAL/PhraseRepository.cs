using HatServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using HatServer.DAL.Interfaces;
using JetBrains.Annotations;
using Model.Entities;

namespace HatServer.DAL
{
    internal sealed class PhraseRepository : Repository<PhraseItem>, IPhraseRepository
    {
        public PhraseRepository([NotNull] ApplicationDbContext context) : base(context)
        {
        }

        public override IEnumerable<PhraseItem> GetAll()
        {
            return Entities.Include(p => p.ReviewStates);
        }

        public override Task<PhraseItem> GetAsync(int id)
        {
            return Entities.Include(p => p.ReviewStates).ThenInclude(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> GetMaxTrackIdAsync()
        {
            var maxTrackId = await Entities.MaxAsync(p => p.TrackId);
            if (maxTrackId < 0)
            {
                maxTrackId++;
            }

            return maxTrackId;
        }

        [ItemCanBeNull]
        public Task<PhraseItem> GetLatestByTrackId(int trackId)
        {
            return Entities.FirstOrDefaultAsync(p => p.TrackId == trackId && p.ClosedBy == null && p.ClosedDate == null);
        }

        public Task<PhraseItem> GetByNameAsync(string phrase)
        {
            return Context.PhraseItems.Include(p => p.ReviewStates).Include(p => p.Pack)
                .FirstOrDefaultAsync(p => string.CompareOrdinal(p.Phrase, phrase) == 0 && p.TrackId > 0);
        }
    }
}
