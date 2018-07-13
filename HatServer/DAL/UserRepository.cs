using System.Threading.Tasks;
using HatServer.Data;
using HatServer.DAL.Interfaces;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace HatServer.DAL
{
    public sealed class UserRepository : Repository<ServerUser>, IUserRepository
    {
        public UserRepository([NotNull] FillerDbContext context) : base(context)
        {
        }

        public Task<ServerUser> GetByNameAsync(string name)
        {
            return Entities.FirstOrDefaultAsync(u => u.UserName == name);
        }
    }
}
