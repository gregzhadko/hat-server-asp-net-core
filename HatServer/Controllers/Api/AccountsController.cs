using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HatServer.DTO.Request;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.Entities;

namespace HatServer.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    public sealed class AccountsController : Controller
    {
        private readonly SignInManager<ServerUser> _signInManager;
        private readonly UserManager<ServerUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountsController(UserManager<ServerUser> userManager, SignInManager<ServerUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<object> Login([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.Single(r => r.UserName == model.Name);
                return GenerateJwtToken(model.Name, appUser);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        //[HttpPost]
        public async Task<object> Register([FromBody] RegisterRequest model)
        {
            //return new object();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
                return GenerateJwtToken(model.Name, user);
            }

            throw new ApplicationException("UNKNOWN_ERROR");
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