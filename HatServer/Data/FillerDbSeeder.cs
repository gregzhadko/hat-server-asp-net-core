using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using OldServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Entities;
using Newtonsoft.Json;

namespace HatServer.Data
{
    [UsedImplicitly]
    internal sealed class FillerDbSeeder : IDbSeeder<FillerDbContext>
    {
        private readonly UserManager<ServerUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMongoServiceClient _mongoServiceClient;

        public FillerDbSeeder(UserManager<ServerUser> userManager, IConfiguration configuration, IMongoServiceClient mongoServiceClient)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mongoServiceClient = mongoServiceClient;
        }

        public void Seed([NotNull] FillerDbContext context)
        {
            context.Database.OpenConnection();

            try
            {
                SeedUsers();
                SeedPacks(context);
            }
            finally
            {
                context.Database.CloseConnection();
            }
        }

        private void SeedUsers()
        {
            var zhadko = new ServerUser {UserName = "zhadko"};
            var fomin = new ServerUser {UserName = "fomin"};
            var sivykh = new ServerUser {UserName = "sivykh"};
            var tatarintsev = new ServerUser {UserName = "tatarintsev"};
            _userManager.CreateAsync(zhadko, _configuration["zhadko"]).Wait();
            _userManager.CreateAsync(fomin, _configuration["fomin"]).Wait();
            _userManager.CreateAsync(sivykh, _configuration["sivykh"]).Wait();
            _userManager.CreateAsync(tatarintsev, _configuration["tatarintsev"]).Wait();
        }

        private void SeedPacks([NotNull] FillerDbContext context)
        {
            var users = _userManager.Users.ToList();

            var packs = _mongoServiceClient.GetAllPacksAsync(users).GetAwaiter().GetResult();

            //SaveToFile(packs);
            var trackIds = packs.SelectMany(p => p.Phrases).Select(p => p.TrackId).ToList();
            var minTrackId = trackIds.Min();
            var maxTrackId = trackIds.Max();

            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Tracks ON");
            for (int i = minTrackId; i <= maxTrackId; i++)
            {
                context.Tracks.Add(new Track {Id = i});
            }

            context.SaveChanges();
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Tracks OFF");

            context.Packs.AddRange(packs);
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Packs ON");
            
            context.SaveChanges();
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Packs OFF");
        }

        private static void SaveToFile(List<Pack> packs)
        {
            var packString = JsonConvert.SerializeObject(packs, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented
            });
            File.AppendAllText($"Backup\\{DateTime.Now:yyyy-MM-dd-hh-mm}.json", packString);
        }
    }
}