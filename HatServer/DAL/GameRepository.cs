using System.Linq;
using System.Threading.Tasks;
using HatServer.Data;
using HatServer.DAL.Interfaces;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace HatServer.DAL
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository([NotNull] GameDbContext context) : base(context)
        {
        }

        public async Task AssignDeviceToUnassignedGamesAsync(DeviceInfo device)
        {
            var unassignedGames = await Entities.Where(g => g.DeviceInfoId == null && g.DeviceInfoGuid == device.DeviceGuid).ToListAsync();
            foreach (var game in unassignedGames)
            {
                game.DeviceInfo = device;
            }

            await Context.SaveChangesAsync();
        }
    }
}