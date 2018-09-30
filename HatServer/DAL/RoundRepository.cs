using HatServer.Data;
using HatServer.DAL.Interfaces;
using JetBrains.Annotations;
using Model.Entities;

namespace HatServer.DAL
{
    public class RoundRepository : Repository<Round>, IRoundRepository
    {
        public RoundRepository([NotNull] GameDbContext context) : base(context)
        {
        }
    }
}