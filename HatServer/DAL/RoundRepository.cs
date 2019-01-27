using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatServer.Data;
using HatServer.DAL.Interfaces;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace HatServer.DAL
{
    public class RoundRepository : Repository<Round>, IRoundRepository
    {
        public RoundRepository([NotNull] GameDbContext context) : base(context)
        {
        }

        public override async Task<List<Round>> GetAllAsync()
        {
            return await Entities.Include(r => r.Words).ToListAsync();
        }

        public async Task<List<Round>> GetNotAttachedRoundsByGameGuidAsync(string gameInGameId)
        {
            return await Entities.Where(r => r.GameId == 0 && r.GameGUID == gameInGameId).ToListAsync();
        }
    }
}