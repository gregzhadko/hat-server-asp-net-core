using System.Collections.Generic;
using FluentValidation;
using JetBrains.Annotations;
using Model.Entities;

namespace HatServer.DTO.Request
{
    public sealed class PostPhraseItemRequest
    {
        [UsedImplicitly]
        public string Phrase { get; set; }

        [UsedImplicitly]
        public double? Complexity { get; set; }

        [UsedImplicitly]
        public string Description { get; set; }

        [UsedImplicitly]
        public int PackId { get; set; }

        [UsedImplicitly]
        public string Author { get; set; }

        [NotNull]
        internal PhraseItem ToPhraseItem(ServerUser user)
        {
            var reviewState = new ReviewState {User = user, State = State.Accept};
            var phraseItem = new PhraseItem
            {
                Phrase = Phrase,
                Complexity = Complexity,
                Description = Description,
                PackId = PackId,
                Version = 1,
                ReviewStates = new List<ReviewState> {reviewState}
            };

            return phraseItem;
        }
    }

    [UsedImplicitly]
    public sealed class PostPhraseItemRequestValidator : AbstractValidator<PostPhraseItemRequest>
    {
        public PostPhraseItemRequestValidator()
        {
            RuleFor(p => p.Phrase).NotEmpty();
            RuleFor(p => p.Author).NotEmpty();
            RuleFor(x => x.PackId).NotEmpty().GreaterThan(0);

            RuleFor(p => p.Complexity).InclusiveBetween(1, 5).When(p => p.Complexity != null);
        }
    }
}