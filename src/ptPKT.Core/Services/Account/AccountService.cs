using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ptPKT.Core.Identity;
using ptPKT.Core.Interfaces.Account;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ptPKT.Core.Exceptions.Indentity;

namespace ptPKT.Core.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountService(UserManager<AppUser> _userManager)
        {
            this._userManager = _userManager;
        }

        public async Task<LoginResult> SignIn(UserLoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                throw new AppUserNotFoundException();

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
                throw new AppUserIncorrectPasswordException();

            var response = new LoginResult()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                Token = GenerateToken(user)
            };

            return response;
        }

        public async Task<LoginResult> SignUp(UserRegistedModel model)
        {
            var newUser = new AppUser
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
                throw new AppUserCreationException(result.Errors);

            var response = new LoginResult()
            {
                Id = newUser.Id,
                FirstName = newUser.FirstName,
                SecondName = newUser.SecondName,
                Token = GenerateToken(newUser)
            };

            return response;
        }

        public async Task SignOut()
        {
            // return Success Result with fake Token ???
        }

        public IEnumerable<AppUser> GetUsers()
        {
            return _userManager.Users;
        }

        public async Task ConfirmEmailAsync(string confirmationToken)
        {
            throw new NotImplementedException();
        }

        public async Task ForgotPasswordAsync(UserLoginModel user)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResult> ResetPasswordAsync(UserLoginModel user)
        {
            throw new NotImplementedException();
        }

        private static string GenerateToken(AppUser user)
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
    }

    public class LoginResult
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }

    public class UserRegistedModel
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string UserName { get; set; } 
        public string Email { get; set; } 
        public string Password { get; set; } 
    }

    public class UserLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
