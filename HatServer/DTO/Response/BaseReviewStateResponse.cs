using JetBrains.Annotations;
using Model.Entities;

namespace HatServer.DTO.Response
{
    public class BaseReviewStateResponse
    {
        public BaseReviewStateResponse([NotNull] ReviewState reviewState)
        {
            UserName = reviewState.User.UserName;
            State = reviewState.State;
            Comment = reviewState.Comment;
        }

        public string UserName { get; set; }

        public State State { get; set; }

        public string Comment { get; set; }
    }
}
