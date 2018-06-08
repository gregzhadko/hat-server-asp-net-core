using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Model.Entities;

namespace HatServer.Data
{
    internal interface IDbInitializer
    {
        void Initialize([NotNull] ApplicationDbContext context, [NotNull] UserManager<ServerUser> userManager,
            [NotNull] IConfiguration configuration);
    }
}