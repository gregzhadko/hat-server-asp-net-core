using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using Model.Entities;

namespace HatServer.DTO.Request
{
    public sealed class PutPhraseItemRequest
    {
        public string Phrase { get; set; }

        public double? Complexity { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public bool ClearReview { get; set; }

        public string Comment { get; set; }

        [NotNull]
        public PhraseItem ToPhraseItem([NotNull] ServerUser user, [NotNull] PhraseItem existingPhrase)
        {
            var newReviewStates = new List<ReviewState>();
            foreach (var state in existingPhrase.ReviewStates)
            {
                if (state.UserId == user.Id)
                {
                    newReviewStates.Add(new ReviewState {UserId = user.Id, State = State.Accept, Comment = Comment});
                }
                else
                {
                    newReviewStates.Add(ClearReview ? new ReviewState {UserId = user.Id, State = State.Unknown} : state.Clone());
                }
            }

            if (!newReviewStates.Select(s => s.UserId).Contains(user.Id))
            {
                newReviewStates.Add(new ReviewState {UserId = user.Id, State = State.Accept, Comment = Comment});
            }

            var phraseItem = new PhraseItem
            {
                PackId = existingPhrase.PackId,
                Phrase = Phrase,
                Complexity = Complexity,
                Description = Description,
                Version = ++existingPhrase.Version,
                TrackId = existingPhrase.TrackId,
                CreatedById = user.Id,
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

            RuleFor(p => p.Complexity).InclusiveBetween(1, 5).When(p => p.Complexity != null);
        }
    }
}
