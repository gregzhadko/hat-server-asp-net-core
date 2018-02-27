using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatServer.Data;
using HatServer.Models;

namespace HatServer.DAL
{
    public class AccountsRepository : Repository<ApplicationUser>
    {
        public AccountsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
