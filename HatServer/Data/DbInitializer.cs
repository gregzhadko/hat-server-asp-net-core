using System.Collections.Generic;
using HatServer.Old;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model;

namespace HatServer.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ServerUser> _userManager;

        public DbInitializer(
            ApplicationDbContext context,
            UserManager<ServerUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //This example just creates an Administrator role and one Admin users
        public void Initialize()
        {
            _context.Database.OpenConnection();

            try
            {
                //SeedUsers();
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

//        private void SeedUsers()
//        {
//            var zhadko = new ServerUser {UserName = "zhadko"};
//            var fomin = new ServerUser {UserName = "fomin"};
//            var sivykh = new ServerUser {UserName = "sivykh"};
//            var tatarintsev = new ServerUser {UserName = "tatarintsev"};
//            _userManager.CreateAsync(zhadko, "8yyyy1C4^xx@").Wait();
//            _userManager.CreateAsync(fomin, "PCd0c%74gNI2").Wait();
//            _userManager.CreateAsync(sivykh, "R22ueOf%#v*!").Wait();
//            _userManager.CreateAsync(tatarintsev, "Qq6t^hJSkr1p").Wait();
//        }

        private void SeedPacks()
        {
            //var users = _userManager.Users.ToList();
            var users = new List<string> {"zhadko", "fomin", "sivykh", "tatarintsev"};
            var packs = OldService.GetAllPacksAsync(users).GetAwaiter().GetResult();

            _context.Packs.AddRange(packs);
        }
    }
}