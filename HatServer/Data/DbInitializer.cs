using System.Linq;
using System.Threading.Tasks;
using HatServer.Models;
using HatServer.Old;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        public async Task InitializeAsync()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.Database.OpenConnection();

            try
            {
                SeedUsers();
                await SeedPacksAsync().ConfigureAwait(false);
                await _context.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT dbo.Packs ON").ConfigureAwait(false);
                _context.SaveChanges();
                await _context.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT dbo.Packs OFF").ConfigureAwait(false);
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

        private async Task SeedPacksAsync()
        {
            var users = _userManager.Users.ToList();
            var service = new OldService();
            var result = await OldService.GetAllPacksAsync(users).ConfigureAwait(false);

            _context.Packs.AddRange(result);
        }
    }
}