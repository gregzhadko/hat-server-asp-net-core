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
        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            if (context.Request.Headers["saveStatistics"] == true)
            {
                //save to bot something;
            }

            await _next(context);
        }
    }
}
