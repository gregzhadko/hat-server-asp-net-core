using System.Collections.Generic;
using FluentValidation;
using HatServer.DAL;
using JetBrains.Annotations;
using Model;

namespace HatServer.DTO.Request
{
    public class PostPhraseItemRequest
    {
        public string Phrase { get; set; }

        public double? Complexity { get; set; }

        public string Description { get; set; }

        public int PackId { get; set; }

        public string Author { get; set; }

        public PhraseItem ToPhraseItem(ServerUser user)
        {
            var reviewState = new ReviewState {User = user, State = State.Accept};
            var phraseItem = new PhraseItem()
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
    public class PostPhraseItemRequestValidator : AbstractValidator<PostPhraseItemRequest>
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