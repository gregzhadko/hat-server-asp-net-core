﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model;
using Model.Entities;
using Newtonsoft.Json;

namespace HatServer.Data
{
    public class GameDbSeeder : IDbSeeder<GameDbContext>
    {
        public void Seed(GameDbContext context)
        {
            try
            {
                context.Database.OpenConnection();
                var packs = new List<GamePack>();

                var exitstingPacks = context.GamePacks.ToList();
            
                var packFiles = Directory.GetFiles(Constants.PacksFolder, "*.json");
                var iconFiles = Directory.GetFiles(Constants.PacksFolder, "*.pdf");
                foreach (var packFile in packFiles.Select(s => File.ReadAllText(s, Encoding.UTF8)))
                {
                    var pack = JsonConvert.DeserializeObject<GamePack>(packFile);
                    if (exitstingPacks.Select(p => p.Id).Contains(pack.Id))
                    {
                        continue;
                    }
                    
                    var iconFile = iconFiles.FirstOrDefault(i => i == $@"Packs\pack_icon_{pack.Id}.pdf");
                    if (iconFile == null)
                    {
                        continue;
                    }
                    
                    pack.GamePackIcon.Icon = File.ReadAllBytes(iconFile);
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