using HatServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HatServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base() { }

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

        public DbSet<HatServer.Models.PhraseItem> PhraseItems { get; set; }

        public DbSet<HatServer.Models.Pack> Packs { get; set; }

        public DbSet<PhraseState> PhraseStates { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Round> Rounds { get; set; }

        public DbSet<RoundPhrase> RoundPhrases { get; set; }

        public DbSet<RoundPhraseState> RoundPhraseStates { get; set; }

        public DbSet<Settings> Settings { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<User> GameUsers { get; set; }
    }
}
