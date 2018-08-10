using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Model;
using Model.Entities;
using Newtonsoft.Json;

namespace HatServer.Data
{
    public class GameDbSeeder : IDbSeeder<GameDbContext>
    {
        private GameDbContext _gameDbContext;
        private IConfiguration _configuration;

        private void SeedDataFromFiles()
        {
            var packs = new List<GamePack>();
            
            var packFiles = Directory.GetFiles(Constants.PacksFolder, "*.json");
            var iconFiles = Directory.GetFiles(Constants.PacksFolder, "*.pdf");
            foreach (var packFile in packFiles.Select(s => File.ReadAllText(s, Encoding.UTF8)))
            {
                var pack = JsonConvert.DeserializeObject<GamePack>(packFile);
                var iconFile = iconFiles.First(i => i == $@"Packs\pack_icon_{pack.Id}.pdf");
                pack.Icon = File.ReadAllBytes(iconFile);
                packs.Add(pack);
            }
        }

        public void Seed(GameDbContext context)
        {
            SeedDataFromFiles();
        }
    }
}