using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Entities;
using Newtonsoft.Json;

namespace HatServer.Data
{
    internal sealed class GameDbSeeder : IDbSeeder<GameDbContext>
    {
        public void Seed([NotNull] GameDbContext context)
        {
            try
            {
                context.Database.OpenConnection();
                var packs = new List<GamePack>();

                var existingPacks = context.GamePacks.Include(p => p.GamePackIcon).ToList();

                var packFiles = Directory.GetFiles(Constants.PacksFolder, "*.json");
                var iconFiles = Directory.GetFiles(Constants.PacksFolder, "*.pdf");
                foreach (var packFile in packFiles.Select(s => File.ReadAllText(s, Encoding.UTF8)))
                {
                    var pack = JsonConvert.DeserializeObject<GamePack>(packFile);
                    var iconFile = iconFiles.FirstOrDefault(i => i == $@"Packs\pack_icon_{pack.Id}.pdf");

                    var existingPack = existingPacks.FirstOrDefault(p => p.Id == pack.Id);
                    if (existingPack != null)
                    {
                        if (existingPack.GamePackIcon == null && iconFile != null)
                        {
                            var existingPackIcon = new GamePackIcon {Icon = File.ReadAllBytes(iconFile), GamePackId = existingPack.Id};
                            context.Add(existingPackIcon);
                        }

                        continue;
                    }

                    if (iconFile != null)
                    {
                        pack.GamePackIcon = new GamePackIcon {Icon = File.ReadAllBytes(iconFile)};
                    }

                    packs.Add(pack);
                }

                context.GamePacks.AddRange(packs);

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.GamePacks ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.GamePacks OFF");
            }
            finally
            {
                context.Database.CloseConnection();
            }
        }
    }
}