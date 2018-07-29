using JetBrains.Annotations;

namespace HatServer.DTO.Response
{
    public sealed class ErrorResponse
    {
        public ErrorResponse(string error, [CanBeNull] object body = null)
        {
            Error = error;
            Body = body;
        }

        [UsedImplicitly]
        public string Error { get; set; }

        [UsedImplicitly]
        public object Body { get; set; }
    }
}