using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatServer.DAL.Interfaces;
using HatServer.DTO.Response;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Entities;
using static HatServer.Tools.BadRequestFactory;

namespace HatServer.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public sealed class PacksController : Controller
    {
        private readonly IPackRepository _packRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<PacksController> _logger;

        public PacksController(IPackRepository packRepository, IUserRepository userRepository, ILogger<PacksController> logger)
        {
            _packRepository = packRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        // GET: api/<controller>
        [ItemNotNull]
        [NotNull]
        [HttpGet("{loadPhrases?}")]
        public async Task<List<BasePackResponse>> GetAll(bool? loadPhrases = null)
        {
            var users = _userRepository.GetAll().ToList();

            IEnumerable<Pack> packs;

            if (loadPhrases == true)
            {
                packs = await _packRepository.GetAllWithPhrases();
            }
            else
            {
                packs = _packRepository.GetAll();
            }

            return packs.Select(p => new BasePackResponse(p, users)).ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return HandleAndReturnBadRequest("Id should be greater than 0", _logger);
            }

            var pack = await _packRepository.GetFullInfoAsync(id);
            if (pack == null)
            {
                return HandleAndReturnBadRequest($"Pack with id {id} wasn't found", _logger);
            }

            var users = _userRepository.GetAll().ToList();
            return Ok(new BasePackResponse(pack, users));
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pack item)
        {
            if (!ModelState.IsValid)
            {
                return HandleAndReturnBadRequest(ModelState, _logger);
            }

            var existing = await _packRepository.GetByNameAsync(item.Name);
            if (existing != null)
            {
                return HandleAndReturnBadRequest($"Pack with name {item.Name} already exists", _logger);
            }

            await _packRepository.InsertAsync(item);
            return CreatedAtRoute("Get", new {id = item.Id}, item);
        }

        //TODO: check it and refactor
        // PUT api/<controller>/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateNameAndDescription(int id, [CanBeNull] [FromBody] string name,
//            [CanBeNull] [FromBody] string description)
//        {
//            if (id == 0 || String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(description))
//            {
//                return BadRequest();
//            }
//
//            var pack = await _packRepository.GetAsync(id);
//            if (pack == null)
//            {
//                return HandleAndReturnBadRequest($"Pack with id {id} wasn't found"));
//            }
//
//            pack.Name = name;
//            pack.Description = description;
//
//            await _packRepository.UpdateAsync(pack);
//
//            return NoContent();
//        }
    }
}