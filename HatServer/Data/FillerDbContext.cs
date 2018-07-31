using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace HatServer.Data
{
    public sealed class FillerDbContext : IdentityDbContext<ServerUser>
    {
        public FillerDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ServerUser> ServerUsers { get; set; }
        public DbSet<ReviewState> ReviewStates { get; set; }

        public DbSet<PhraseItem> PhraseItems { get; set; }

        public DbSet<Pack> Packs { get; set; }
    }
}
