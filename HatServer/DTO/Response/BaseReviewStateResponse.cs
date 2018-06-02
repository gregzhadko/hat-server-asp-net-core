using JetBrains.Annotations;
using Model.Entities;

namespace HatServer.DTO.Response
{
    public sealed class BaseReviewStateResponse
    {
        internal BaseReviewStateResponse([NotNull] ReviewState reviewState)
        {
            Author = reviewState.User.UserName;
            Status = reviewState.State;
            Comment = reviewState.Comment;
        }

        [UsedImplicitly]
        public string Author { get; set; }

        [UsedImplicitly]
        public State Status { get; set; }

        [UsedImplicitly]
        public string Comment { get; set; }
    }
}
