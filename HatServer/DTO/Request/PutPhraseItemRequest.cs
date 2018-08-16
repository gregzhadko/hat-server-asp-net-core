using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using JetBrains.Annotations;
using Model.Entities;

namespace HatServer.DTO.Request
{
    public sealed class PutPhraseItemRequest
    {
        [UsedImplicitly]
        public string Phrase { get; set; }

        [UsedImplicitly]
        public double? Complexity { get; set; }

        [UsedImplicitly]
        public string Description { get; set; }

        [UsedImplicitly]
        public string Author { get; set; }

        [UsedImplicitly]
        public bool ClearReview { get; set; }

        [UsedImplicitly]
        public string Comment { get; set; }

        public int Version { get; set; }

        [NotNull]
        internal PhraseItem ToPhraseItem([NotNull] ServerUser authorUser, [NotNull] PhraseItem existingPhrase)
        {
            var newReviewStates = new List<ReviewState>();
            foreach (var state in existingPhrase.ReviewStates)
            {
                if (state.UserId == authorUser.Id)
                {
                    newReviewStates.Add(new ReviewState {UserId = authorUser.Id, State = State.Accept, Comment = Comment});
                }
                else
                {
                    newReviewStates.Add(ClearReview ? new ReviewState {UserId = state.UserId, State = State.Unknown} : state.Clone());
                }
            }

            if (!newReviewStates.Select(s => s.UserId).Contains(authorUser.Id))
            {
                newReviewStates.Add(new ReviewState {UserId = authorUser.Id, State = State.Accept, Comment = Comment});
            }

            var phraseItem = new PhraseItem
            {
                PackId = existingPhrase.PackId,
                Phrase = Phrase,
                Complexity = Complexity,
                Description = Description,
                Version = existingPhrase.Version + 1,
                TrackId = existingPhrase.TrackId,
                CreatedById = authorUser.Id,
                CreatedDate = DateTime.Now,
                ReviewStates = newReviewStates
            };

            return phraseItem;
        }
    }

    [UsedImplicitly]
    public sealed class PutPhraseItemRequestValidator : AbstractValidator<PutPhraseItemRequest>
    {
        public PutPhraseItemRequestValidator()
        {
            RuleFor(p => p.Phrase).NotEmpty();
            RuleFor(p => p.Author).NotEmpty();
            RuleFor(p => p.Version).GreaterThanOrEqualTo(0);

            RuleFor(p => p.Complexity).InclusiveBetween(1, 5).When(p => p.Complexity != null);
        }
    }
}
