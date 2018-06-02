using System;
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
            return Entities.Include(p => p.ReviewStates).ThenInclude(p => p.User);
        }

        public override Task<PhraseItem> GetAsync(int id)
        {
            return Entities.Include(p => p.ReviewStates).ThenInclude(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CloseAndInsert([NotNull] PhraseItem newPhrase, [NotNull] PhraseItem oldPhrase, string userId)
        {
            oldPhrase.ClosedById = userId;
            oldPhrase.ClosedDate = DateTime.Now;
            Entities.Update(oldPhrase);
            Entities.Add(newPhrase);
            await Context.SaveChangesAsync();
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
            return Entities.Include(p => p.ReviewStates).ThenInclude(s => s.User)
                .FirstOrDefaultAsync(p => p.TrackId == trackId && p.ClosedBy == null && p.ClosedDate == null);
        }

        public Task<PhraseItem> GetByNameAsync(string phrase)
        {
            return Entities.Include(p => p.ReviewStates).Include(p => p.Pack)
                .FirstOrDefaultAsync(p => string.CompareOrdinal(p.Phrase, phrase) == 0 && p.TrackId > 0);
        }
    }
}
