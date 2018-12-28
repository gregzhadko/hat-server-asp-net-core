using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
                .Include(g => g.Teams).ThenInclude(t => t.Players);
        }

        public List<FullGame> GetFullGames()
        {
            var originalRounds = Context.Set<Round>().Include(r => r.Words).ToList();
            var groupedRounds = originalRounds.GroupBy(r => r.GameId).ToList();

            var games = Entities
                .Include(g => g.Words)
                .Include(g => g.Teams).ThenInclude(t => t.Players)
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
    }
}