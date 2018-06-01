using System.Threading.Tasks;
using HatServer.Data;
using HatServer.DAL.Interfaces;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace HatServer.DAL
{
    internal sealed class UserRepository : Repository<ServerUser>, IUserRepository
    {
        public UserRepository([NotNull] ApplicationDbContext context) : base(context)
        {
        }

        public Task<ServerUser> GetByNameAsync(string name)
        {
            return Entities.FirstOrDefaultAsync(u => u.UserName == name);
        }
    }
}
