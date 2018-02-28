using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatServer.Models;
using Microsoft.AspNetCore.Identity;

namespace HatServer.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //This example just creates an Administrator role and one Admin users
        public void Initialize()
        {
            //create database schema if none exists
            _context.Database.EnsureCreated();

            var zhadko = new ApplicationUser { UserName = "zhadko" };
            var fomin = new ApplicationUser { UserName = "fomin" };
            var sivykh = new ApplicationUser { UserName = "sivykh" };
            var tatarintsev = new ApplicationUser { UserName = "tatarintsev" };
            _userManager.CreateAsync(zhadko, "8yyyy1C4^xx@").Wait();
            _userManager.CreateAsync(fomin, "PCd0c%74gNI2").Wait();
            _userManager.CreateAsync(sivykh, "R22ueOf%#v*!").Wait();
            _userManager.CreateAsync(tatarintsev, "Qq6t^hJSkr1p").Wait();
        }
    }

    public interface IDbInitializer
    {
        void Initialize();
    }
}
