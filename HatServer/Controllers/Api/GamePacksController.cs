using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HatServer.DTO.Response;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HatServer.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public sealed class GamePacksController : Controller
    {
        private const string PacksFolder = "Packs";

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "Get_Game_Packs")]
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