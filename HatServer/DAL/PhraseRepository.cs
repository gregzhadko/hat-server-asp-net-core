using System;
using HatServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using HatServer.DAL.Interfaces;
using HatServer.DTO.Request;
using JetBrains.Annotations;
using Model.Entities;

namespace HatServer.DAL
{
    internal sealed class PhraseRepository : Repository<PhraseItem>, IPhraseRepository
    {
        public PhraseRepository([NotNull] FillerDbContext context) : base(context)
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

        [ItemNotNull]
        public async Task<PhraseItem> UpdatePhraseAsync([NotNull] PutPhraseItemRequest request, [NotNull] ServerUser user,
            [NotNull] PhraseItem existingPhrase)
        {
            var cloned = existingPhrase.Clone(); //request.ToPhraseItem(user, existingPhrase).FormatPhrase();
            cloned.Phrase = string.IsNullOrWhiteSpace(request.Phrase) ? cloned.Phrase : request.Phrase;
            cloned.Complexity = request.Complexity < 0 || request.Complexity > 0 ? cloned.Complexity : request.Complexity;
            cloned.Description = request.Description ?? cloned.Description;
            cloned.Version++;

            var reviewState = new ReviewState {Comment = request.Comment, State = State.Accept, UserId = user.Id};
            UpdateReviewStates(cloned, reviewState, user.Id, request.ClearReview);

            await CloseAndInsertAsync(cloned, existingPhrase, user.Id);
            return cloned;
        }

        private Task CloseAndInsertAsync([NotNull] PhraseItem newPhrase, [NotNull] PhraseItem oldPhrase, [NotNull] string userId)
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

        [ItemNotNull]
        public async Task<PhraseItem> AddReviewAsync([NotNull] PhraseItem phrase, [NotNull] ServerUser user, [NotNull] PostReviewRequest request)
        {
            var reviewState = new ReviewState {Comment = request.Comment, State = request.Status, UserId = user.Id};
            var cloned = phrase.Clone();
            cloned.Version++;
            UpdateReviewStates(cloned, reviewState, user.Id, request.ClearReview);

            await CloseAndInsertAsync(cloned, phrase, user.Id);
            return cloned;
        }

        private static void UpdateReviewStates([NotNull] PhraseItem phraseItem, ReviewState reviewState, string userId, bool clearReview)
        {
            if (clearReview)
            {
                phraseItem.ReviewStates.Clear();
                phraseItem.ReviewStates.Add(reviewState);
            }
            else
            {
                var index = phraseItem.ReviewStates.FindIndex(r => r.UserId == userId);
                if (index >= 0)
                {
                    phraseItem.ReviewStates[index] = reviewState;
                }
                else
                {
                    phraseItem.ReviewStates.Add(reviewState);
                }
            }
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