using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace HatServer.Data
{
    public sealed class FillerDbContext : IdentityDbContext<ServerUser>
    {
        // ReSharper disable once SuggestBaseTypeForParameter (Requires for migrations generations)
        public FillerDbContext(DbContextOptions<FillerDbContext> options)
            : base(options)
        {
        }

        public DbSet<ServerUser> ServerUsers { get; set; }
        public DbSet<ReviewState> ReviewStates { get; set; }

        public DbSet<PhraseItem> PhraseItems { get; set; }

        public DbSet<Pack> Packs { get; set; }
    }
}
