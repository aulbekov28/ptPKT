using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ptPKT.Core.Identity;
using ptPKT.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ptPKT.WebUI.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] LoginModelDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("Wrong email or password");

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
                return BadRequest("Wrong email or password");

            var response = new LoginResponseModel()
            {
                Id = user.Id,
                Email = user.Email,
                FistName = user.FirstName,
                SecondName = user.SecondName,
                Token = GenerateToken(user)
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] RegisterModelDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newUser = new AppUser
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors.First().Description);

            var response = new LoginResponseModel()
            {
                Id = newUser.Id,
                Email = newUser.Email,
                FistName = newUser.FirstName,
                SecondName = newUser.SecondName,
                Token = GenerateToken(newUser)
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            throw new NotImplementedException();
        }


        private string GenerateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim("name", user.UserName),
                new Claim("id", user.Id.ToString())
            };

            var signingKey =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")));
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(60);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet]
        public IActionResult UserList()
        {
            return Ok(_userManager.Users.ToList());
        }

        [HttpGet]
        [Authorize]
        public IActionResult Check()
        {
            var userId = User.Claims.First(u => u.Type == "id").Value;
            var _appUser = _userManager.FindByIdAsync(userId).Result;
            return Ok(_appUser);
        }
    }
}