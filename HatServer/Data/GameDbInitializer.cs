using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Model;
using Model.Entities;
using Newtonsoft.Json;

namespace HatServer.Data
{
    public class GameDbInitializer : IGameDbInitializer
    {
        private GameDbContext _gameDbContext;
        private IConfiguration _configuration;

        public void Initialize(GameDbContext contex, IConfiguration configuration)
        {
            _configuration = configuration;
            _gameDbContext = contex;
            SeedData();
        }

        private void SeedData()
        {
            SeedDataFromFile();
        }

        private void SeedDataFromFile()
        {
            var packFiles = Directory.GetFiles(Constants.PacksFolder, "*.json");
            var iconFiles = Directory.GetFiles(Constants.PacksFolder, "*.pdf");
            foreach (var pack in packFiles.Select(s => File.ReadAllText(s, Encoding.UTF8))
                .Select(JsonConvert.DeserializeObject<GamePack>))
            {
                
            }
        }
    }

    public interface IGameDbInitializer
    {
    }
}