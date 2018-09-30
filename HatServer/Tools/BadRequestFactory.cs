using System.Linq;
using System.Text;
using HatServer.DTO.Response;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace HatServer.Tools
{
    public static class BadRequestFactory
    {
        [NotNull]
        public static BadRequestObjectResult HandleAndReturnBadRequest<T>(string message, ILogger<T> logger)
        {
            logger.LogWarning(message);
            var errorResponse = new ErrorResponse(message);
            return new BadRequestObjectResult(errorResponse);
        }

        [NotNull]
        public static BadRequestObjectResult HandleAndReturnBadRequest<T>([NotNull] ModelStateDictionary modelState, ILogger<T> logger)
        {
            return HandleAndReturnBadRequest(ParseModelState(modelState), logger);
        }

        [NotNull]
        private static string ParseModelState([NotNull] ModelStateDictionary modelState)
        {
            var errorResult = new StringBuilder();
            foreach (var modelError in modelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))
            {
                errorResult.Append(modelError);
            }

            return errorResult.ToString();
        }
    }
}