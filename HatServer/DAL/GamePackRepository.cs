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
    public class GamePackRepository : Repository<GamePack>, IGamePackRepository
    {
        public GamePackRepository([NotNull] GameDbContext context) : base(context)
        {
        }

        public override IEnumerable<GamePack> GetAll()
        {
            return Entities.Include(p => p.Phrases).ToList();
        }

        public override Task<GamePack> GetAsync(int id)
        {
            return Entities.Include(p => p.Phrases).FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<GamePackIcon> GetPackIcon(int packId)
        {
            return Context.Set<GamePackIcon>().FirstOrDefaultAsync(g => g.GamePackId == packId);
        }
    }
}