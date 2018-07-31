using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HatServer.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Newtonsoft.Json;

namespace HatServer.Controllers.Api
{
    //[Authorize]
    [Route("api/[controller]")]
    public sealed class GamePacksController : Controller
    {
        private const string PacksFolder = "Packs";

        // GET api/<controller>
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = new List<GamePack>();
            var files = Directory.GetFiles(PacksFolder, "*.json");
            foreach (var pack in files.Select(s => System.IO.File.ReadAllText(s, Encoding.UTF8))
                .Select(JsonConvert.DeserializeObject<GamePack>))
            {
                pack.Count = pack.Phrases.Count;
                pack.Phrases = null;
                result.Add(pack);
            }

            return Ok(result);
        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "Get_Game_Pack")]
        public async Task<IActionResult> Get(int id)
        {
            var file = Directory.GetFiles(PacksFolder, "*.json")
                .FirstOrDefault(f => f.EndsWith($"{id}.json", StringComparison.Ordinal));
            if (file == null)
            {
                return BadRequest(new ErrorResponse($"Pack with id = {id} wasn't found"));
            }

            var result = await System.IO.File.ReadAllTextAsync($"{file}");
            return Ok(result);
        }

        // GET api/<controller>/5
        [HttpGet("{id}/icon", Name = "Get_icon")]
        public IActionResult GetIcon(int id)
        {
            var file = Directory.GetFiles(PacksFolder, "*.pdf")
                .FirstOrDefault(f => f.EndsWith($"{id}.pdf", StringComparison.Ordinal));
            if (file == null)
            {
                return BadRequest(new ErrorResponse($"Pack with id = {id} wasn't found"));
            }

            var fileStream = new FileStream(file, FileMode.Open);
            return File(fileStream, "application/file", "icon.pdf");
        }
    }
}