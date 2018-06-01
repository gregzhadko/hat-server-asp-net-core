using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace HatServer.Data
{
    public sealed class ApplicationDbContext : IdentityDbContext<ServerUser>
    {
        public ApplicationDbContext() : base()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<ServerUser> ServerUsers { get; set; }
        public DbSet<ReviewState> ReviewStates { get; set; }

        public DbSet<PhraseItem> PhraseItems { get; set; }

        public DbSet<Pack> Packs { get; set; }
        
        public DbSet<Game> Games { get; set; }

        //public DbSet<Stage> Stages { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Round> Rounds { get; set; }

        public DbSet<RoundPhrase> RoundPhrases { get; set; }

        public DbSet<RoundPhraseState> RoundPhraseStates { get; set; }

        public DbSet<Settings> Settings { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<User> GameUsers { get; set; }
    }
}
