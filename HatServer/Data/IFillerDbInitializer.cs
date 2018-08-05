using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Model.Entities;

namespace HatServer.Data
{
    internal interface IFillerDbInitializer
    {
        void Initialize([NotNull] FillerDbContext context, [NotNull] UserManager<ServerUser> userManager,
            [NotNull] IConfiguration configuration);
    }
}