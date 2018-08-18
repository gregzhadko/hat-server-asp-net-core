using HatServer.Data;
using HatServer.DAL.Interfaces;
using JetBrains.Annotations;
using Model.Entities;

namespace HatServer.DAL
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        protected GameRepository([NotNull] GameDbContext context) : base(context)
        {
        }
    }
}