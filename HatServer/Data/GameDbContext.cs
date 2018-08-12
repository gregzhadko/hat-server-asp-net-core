using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace HatServer.Data
{
    public class GameDbContext : DbContext
    {
        // ReSharper disable once SuggestBaseTypeForParameter (Requires for migrations generations)
        public GameDbContext(DbContextOptions<GameDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<DownloadedPacksInfo> DownloadedPacksInfos { get; set; }

        public DbSet<GamePack> GamePacks { get; set; }
        
        public DbSet<GamePackIcon> GamePackIcons { get; set; }

        public DbSet<GamePhrase> GamePhrases { get; set; }
        
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