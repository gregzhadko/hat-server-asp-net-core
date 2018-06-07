using System.Linq;
using JetBrains.Annotations;
using OldServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace HatServer.Data
{
    [UsedImplicitly]
    internal sealed class DbInitializer : IDbInitializer
    {
        private ApplicationDbContext _context;
        private UserManager<ServerUser> _userManager;

        //This example just creates an Administrator role and one Admin users
        public void Initialize(ApplicationDbContext context, UserManager<ServerUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _context.Database.OpenConnection();

            try
            {
                SeedUsers();
                SeedPacks();
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Packs ON");
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Packs OFF");
            }
            finally
            {
                _context.Database.CloseConnection();
            }
        }

        private void SeedUsers()
        {
            var zhadko = new ServerUser {UserName = "zhadko"};
            var fomin = new ServerUser {UserName = "fomin"};
            var sivykh = new ServerUser {UserName = "sivykh"};
            var tatarintsev = new ServerUser {UserName = "tatarintsev"};
            _userManager.CreateAsync(zhadko, "8yyyy1C4^xx@").Wait();
            _userManager.CreateAsync(fomin, "PCd0c%74gNI2").Wait();
            _userManager.CreateAsync(sivykh, "R22ueOf%#v*!").Wait();
            _userManager.CreateAsync(tatarintsev, "Qq6t^hJSkr1p").Wait();
        }

        private void SeedPacks()
        {
            var users = _userManager.Users.ToList();

            var packs = MongoServiceClient.GetAllPacksAsync(users).GetAwaiter().GetResult();

            _context.Packs.AddRange(packs);
        }
    }
}