using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace HatServer.Middleware
{
    public sealed class StatisticsMiddleware
    {
        private readonly RequestDelegate _next;

        public StatisticsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        [UsedImplicitly]
        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        public Task Invoke([NotNull] HttpContext context /* other dependencies */)
        {
            var requestHeader = context.Request.Headers["SaveStatistics"];

            if (requestHeader.Count > 0 && requestHeader[0] == "true")
            {
                //save to bot something;
            }

            return _next(context);
        }
    }
}
