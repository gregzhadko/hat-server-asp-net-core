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

        public Task CloseAndInsertAsync(PhraseItem newPhrase, PhraseItem oldPhrase, [NotNull] string userId)
        {
            ClosePhrase(oldPhrase, userId);
            Entities.Add(newPhrase);
            return Context.SaveChangesAsync();
        }

        public Task DeleteAsync([CanBeNull] PhraseItem phrase, [NotNull] string userId)
        {
            if (phrase == null)
            {
                return Task.CompletedTask;
            }

            ClosePhrase(phrase, userId);
            return Context.SaveChangesAsync();
        }

        private void ClosePhrase([NotNull] PhraseItem phrase, [NotNull] string userId)
        {
            phrase.ClosedById = userId;
            phrase.ClosedDate = DateTime.Now;
            Entities.Update(phrase);
        }

        public Task<PhraseItem> GetByNameExceptTrackIdAsync(string phrase, int trackId)
        {
            return Entities.Include(p => p.Pack).FirstOrDefaultAsync(p => p.TrackId != trackId && p.Phrase == phrase);
        }

        public async Task<int> GetMaxTrackIdAsync()
        {
            var maxTrackId = await Entities.MaxAsync(p => p.TrackId);
            if (maxTrackId < 0)
            {
                maxTrackId = 0;
            }

            return maxTrackId;
        }

        [ItemCanBeNull]
        public Task<PhraseItem> GetLatestByTrackIdAsync(int trackId)
        {
            return Entities.Include(p => p.ReviewStates).ThenInclude(s => s.User)
                .FirstOrDefaultAsync(p => p.TrackId == trackId && p.ClosedBy == null && p.ClosedDate == null);
        }

        public Task<PhraseItem> GetByNameAsync(string phrase)
        {
            return Entities.Include(p => p.ReviewStates).Include(p => p.Pack).FirstOrDefaultAsync(p =>
                string.CompareOrdinal(p.Phrase, phrase) == 0 && p.TrackId > 0 && p.ClosedBy == null &&
                p.ClosedDate == null);
        }
    }
}
