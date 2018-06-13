using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using Model.Entities;

namespace HatServer.DTO.Request
{
    public sealed class PostReviewRequest
    {
        [UsedImplicitly]
        public string Author { get; set; }

        [UsedImplicitly]
        public bool ClearReview { get; set; }

        [UsedImplicitly]
        public string Comment { get; set; }

        [UsedImplicitly]
        public State Status { get; set; }
    }

    [UsedImplicitly]
    public sealed class PostReviewRequestValidator : AbstractValidator<PostReviewRequest>
    {
        public PostReviewRequestValidator()
        {
            RuleFor(p => p.Author).NotEmpty();
            RuleFor(p => p.Status).IsInEnum();
        }
    }
}
