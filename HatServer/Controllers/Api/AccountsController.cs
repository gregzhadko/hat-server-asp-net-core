using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HatServer.DTO.Request;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Model.Entities;
using static HatServer.Tools.BadRequestFactory;

namespace HatServer.Controllers.Api
{
    /// <summary>
    /// Contains the logic which works with accounts
    /// </summary>
    [Route("Api/[controller]/[action]")]
    public sealed class AccountsController : Controller
    {
        private readonly SignInManager<ServerUser> _signInManager;
        private readonly UserManager<ServerUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountsController> _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public AccountsController(UserManager<ServerUser> userManager, SignInManager<ServerUser> signInManager,
            IConfiguration configuration, ILogger<AccountsController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Handles login request and generates token which can be used in the future requests for user's authorization 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Authorized</response>
        /// <response code="400">Request body is incorrect</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return HandleAndReturnBadRequest(ModelState, _logger);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, false, false);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            var appUser = await _userManager.Users.SingleAsync(r => r.UserName == model.Name);
            var token = GenerateJwtToken(model.Name, appUser);
            return Ok(new { token });
        }

        private object GenerateJwtToken(string email, [NotNull] IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}