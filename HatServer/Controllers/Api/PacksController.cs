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
    /// <inheritdoc />
    /// <summary>
    /// Contains API methods to work with packs for the filler .
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public sealed class PacksController : Controller
    {
        private readonly IPackRepository _packRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<PacksController> _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="packRepository"></param>
        /// <param name="userRepository"></param>
        /// <param name="logger"></param>
        public PacksController(IPackRepository packRepository, IUserRepository userRepository, ILogger<PacksController> logger)
        {
            _packRepository = packRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        /// <summary>
        /// Gets all packs for the filler.
        /// </summary>
        /// <param name="loadPhrases">Optional: true - if you want to get all phrases of the packs (works slower), false - otherwise</param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns filler packs data by its id
        /// </summary>
        /// <param name="id">Id of tha pack to download</param>
        /// <response code="400">There is no pack with this id in the database</response>
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

        //TODO: replace Pack with PackDTO or PackRequest. The way it works right now is incorrect.
        [HttpPost]
        private async Task<IActionResult> Create([FromBody] Pack item)
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
    }
}