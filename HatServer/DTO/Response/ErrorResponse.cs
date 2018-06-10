using JetBrains.Annotations;

namespace HatServer.DTO.Response
{
    public sealed class ErrorResponse
    {
        public ErrorResponse(string error)
        {
            Error = error;
        }

        [UsedImplicitly]
        public string Error { get; set; }
    }
}
