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
    public sealed class GamePackRepository : Repository<GamePack>, IGamePackRepository
    {
        public GamePackRepository([NotNull] GameDbContext context) : base(context)
        {
        }

        [NotNull]
        public override async Task<List<GamePack>> GetAllAsync()
        {
            return await Entities.Include(p => p.Phrases).ToListAsync();
        }

        public override async Task<GamePack> GetAsync(int id)
        {
            return await Entities.Include(p => p.Phrases).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<GamePackIcon> GetPackIconAsync(int packId)
        {
            return await Context.Set<GamePackIcon>().FirstOrDefaultAsync(g => g.GamePackId == packId);
        }
    }
}