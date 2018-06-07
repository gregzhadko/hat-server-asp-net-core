using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Model.Entities;

namespace HatServer.DTO.Response
{
    public sealed class BaseReviewStateResponse
    {
        internal BaseReviewStateResponse([NotNull] ReviewState reviewState, [CanBeNull] IEnumerable<ServerUser> users)
        {
            //TODO: do it better
            Author = reviewState.User != null ? reviewState.User.UserName : users?.FirstOrDefault(u => u.Id == reviewState.UserId)?.UserName;
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
