using HatServer.Data;
using HatServer.Models;
using System;
using System.Linq;

namespace HatServer.DAL
{
    public class AccountsRepository : Repository<ServerUser>
    {
        public AccountsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
