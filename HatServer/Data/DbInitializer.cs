using HatServer.Models;
using HatServer.Old;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatServer.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DbInitializer(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //This example just creates an Administrator role and one Admin users
        public async Task InitializeAsync()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.Database.OpenConnection();

            try
            {
                SeedUsers();
                await SeedPacksAsync().ConfigureAwait(false);
                await _context.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT dbo.Pack ON").ConfigureAwait(false);
                _context.SaveChanges();
                await _context.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT dbo.Pack OFF").ConfigureAwait(false);
            }
            finally
            {
                _context.Database.CloseConnection();
            }

        }

        private void SeedUsers()
        {
            var zhadko = new ApplicationUser { UserName = "zhadko" };
            var fomin = new ApplicationUser { UserName = "fomin" };
            var sivykh = new ApplicationUser { UserName = "sivykh" };
            var tatarintsev = new ApplicationUser { UserName = "tatarintsev" };
            _userManager.CreateAsync(zhadko, "8yyyy1C4^xx@").Wait();
            _userManager.CreateAsync(fomin, "PCd0c%74gNI2").Wait();
            _userManager.CreateAsync(sivykh, "R22ueOf%#v*!").Wait();
            _userManager.CreateAsync(tatarintsev, "Qq6t^hJSkr1p").Wait();
        }

        private async Task SeedPacksAsync()
        {
            var users = _userManager.Users.ToList();
            var service = new OldService();
            var result = await OldService.GetAllPacksAsync(users).ConfigureAwait(false);

            _context.Pack.AddRange(result);

        }
    }
}
