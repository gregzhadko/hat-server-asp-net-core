using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace HatServer.Data
{
    public class ProductionDbContext : DbContext
    {
        public ProductionDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public GamePack GamePack { get; set; }

        public GamePhrase GamePhrase { get; set; }
        
        public DbSet<Game> Games { get; set; }

        public DbSet<Stage> Stages { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Round> Rounds { get; set; }

        public DbSet<RoundPhrase> RoundPhrases { get; set; }

        public DbSet<RoundPhraseState> RoundPhraseStates { get; set; }

        public DbSet<Settings> Settings { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<User> GameUsers { get; set; }
    }
}