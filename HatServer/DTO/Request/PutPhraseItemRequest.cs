using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using Model.Entities;
using MoreLinq;

namespace HatServer.DTO.Request
{
    public class PutPhraseItemRequest
    {
        public string Phrase { get; set; }

        public double? Complexity { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public bool ClearReview { get; set; }

        public string Comment { get; set; }

        [NotNull]
        public PhraseItem ToPhraseItem(ServerUser user, [NotNull] PhraseItem existingPhrase)
        {
            var newReviewStates = new List<ReviewState>();
            foreach (var state in existingPhrase.ReviewStates)
            {
                ReviewState newState;
                if (state.UserId == user.Id)
                {
                    newState = new ReviewState {UserId = user.Id, State = State.Accept, Comment = Comment};
                }
                else
                {
                    newState = ClearReview ? new ReviewState {UserId = state.UserId, State = State.Unknown} : state.Clone();
                }

                newReviewStates.Add(newState);
            }

            var phraseItem = new PhraseItem
            {
                Phrase = Phrase,
                Complexity = Complexity,
                Description = Description,
                Version = ++existingPhrase.Version,
                ReviewStates = newReviewStates
            };

            return phraseItem;
        }
    }

    [UsedImplicitly]
    public class PutPhraseItemRequestValidator : AbstractValidator<PutPhraseItemRequest>
    {
        public PutPhraseItemRequestValidator()
        {
            RuleFor(p => p.Phrase).NotEmpty();
            RuleFor(p => p.Author).NotEmpty();

            RuleFor(p => p.Complexity).InclusiveBetween(1, 5).When(p => p.Complexity != null);
        }
    }
}
