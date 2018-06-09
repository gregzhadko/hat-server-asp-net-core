using System.Linq;
using JetBrains.Annotations;
using OldServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Entities;

namespace HatServer.Data
{
    [UsedImplicitly]
    internal sealed class DbInitializer : IDbInitializer
    {
        private ApplicationDbContext _context;
        private UserManager<ServerUser> _userManager;
        private IConfiguration _configuration;

        //This example just creates an Administrator role and one Admin users
        public void Initialize(ApplicationDbContext context, UserManager<ServerUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            SeedData();
        }

        private void SeedData()
        {
            _context.Database.OpenConnection();

            try
            {
                SeedUsers();
                SeedPacks();
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
            _userManager.CreateAsync(zhadko, _configuration["zhadko"].ToString()).Wait();
            _userManager.CreateAsync(fomin, _configuration["fomin"].ToString()).Wait();
            _userManager.CreateAsync(sivykh, _configuration["sivykh"]).Wait();
            _userManager.CreateAsync(tatarintsev, _configuration["tatarintsev"]).Wait();
            _context.SaveChanges();
        }

        private void SeedPacks()
        {
            var users = _userManager.Users.ToList();

            var packs = MongoServiceClient.GetAllPacksAsync(users).GetAwaiter().GetResult();

            _context.Packs.AddRange(packs);
            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Packs ON");
            _context.SaveChanges();
            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Packs OFF");
        }
    }
}