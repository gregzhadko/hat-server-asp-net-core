using HatServer.Data;
using JetBrains.Annotations;
using Model.Entities;

namespace HatServer.DAL
{
    public class GamePackRepository : Repository<GamePack>
    {
        protected GamePackRepository([NotNull] FillerDbContext context) : base(context)
        {
        }
        
    }
}