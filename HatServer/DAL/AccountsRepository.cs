using HatServer.Data;
using HatServer.Models;

namespace HatServer.DAL
{
    public class AccountsRepository : Repository<ServerUser>
    {
        public AccountsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
