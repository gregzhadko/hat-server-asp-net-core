using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HatServer.DTO.Request;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Model.Entities;
using static HatServer.Tools.BadRequestFactory;

namespace HatServer.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    public sealed class AccountsController : Controller
    {
        private readonly SignInManager<ServerUser> _signInManager;
        private readonly UserManager<ServerUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(UserManager<ServerUser> userManager, SignInManager<ServerUser> signInManager,
            IConfiguration configuration, ILogger<AccountsController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return HandleAndReturnBadRequest(ModelState, _logger);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.Single(r => r.UserName == model.Name);
                var token = GenerateJwtToken(model.Name, appUser);
                return Ok(new {token});
            }

            return HandleAndReturnBadRequest("INVALID_LOGIN_ATTEMPT", _logger);
        }

        //[HttpPost]
        public async Task<object> Register([FromBody] RegisterRequest model)
        {
            //return new object();

            if (!ModelState.IsValid)
            {
                return HandleAndReturnBadRequest(ModelState, _logger);
            }

            var user = new ServerUser
            {
                UserName = model.Name,
                Email = model.Name
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                var token = GenerateJwtToken(model.Name, user);
                return Ok(new {token});
            }

            return HandleAndReturnBadRequest("INVALID_LOGIN_ATTEMPT", _logger);
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