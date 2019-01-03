using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HatServer.Data;
using HatServer.DAL.Interfaces;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Entities;

namespace HatServer.DAL
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository([NotNull] GameDbContext context) : base(context)
        {
        }

        public Game GetGameByGuid(string roundGameGUID)
        {
            return Entities.FirstOrDefault(g => g.InGameId == roundGameGUID);
        }

        public override IEnumerable<Game> GetAll()
        {
            return Entities
                .Include(g => g.Words)
                .Include(g => g.Teams).ThenInclude(t => t.Players)
                .AsNoTracking();
        }

        public IEnumerable<FullGame> GetFullGames()
        {
            var originalRounds = Context.Set<Round>()
                .Include(r => r.Words)
                .AsNoTracking()
                .ToList();
            
            var groupedRounds = originalRounds.GroupBy(r => r.GameId).ToList();

            var games = Entities
                .Include(g => g.Words)
                .Include(g => g.Teams).ThenInclude(t => t.Players)
                .AsNoTracking()
                .ToList();
            
            var result = new List<FullGame>();
            foreach (var game in games)
            {
                var group = groupedRounds.FirstOrDefault(r => r.Key == game.Id);
                if (group == null)
                {
                    continue;
                }

                var rounds = group.Select(g => g).ToList();
                result.Add(new FullGame{Game = game, Rounds = rounds});
            }

            return result;
        }

        public async Task<FullGame> GetFullGameAsync(int id)
        {
            var game = await Entities
                .Include(g => g.Words)
                .Include(g => g.Teams).ThenInclude(t => t.Players)
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                return null;
            }

            var rounds = Context.Set<Round>().Where(r => r.GameId == id).Include(r => r.Words).ToList();
            return new FullGame {Game = game, Rounds = rounds};
        }
    }
}