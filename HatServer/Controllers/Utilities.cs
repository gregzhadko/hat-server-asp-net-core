using System.Linq;
using System.Text;
using HatServer.DTO.Response;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HatServer.Controllers
{
    internal static class Utilities
    {
        [NotNull]
        internal static ErrorResponse ParseErrors([NotNull] this ModelStateDictionary modelState)
        {
            var errorResult = new StringBuilder();
            foreach (var modelError in modelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))
            {
                errorResult.Append(modelError);
                errorResult.Append(". ");
            }

            return new ErrorResponse(errorResult.ToString());
        }
    }
}
