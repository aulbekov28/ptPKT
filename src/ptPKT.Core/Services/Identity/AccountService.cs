using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ptPKT.Core.Exceptions.Indentity;
using ptPKT.Core.Identity;
using ptPKT.Core.Interfaces.Identity;

namespace ptPKT.Core.Services.Identity
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserLoginResult> SignIn(UserLoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                throw new AppUserNotFoundException();

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
                throw new AppUserIncorrectPasswordException();

            var response = new UserLoginResult()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                Token = GenerateToken(user)
            };

            return response;
        }

        public async Task<UserLoginResult> SignUp(UserRegistedModel model)
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

            var response = new UserLoginResult()
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

        public async Task<UserLoginResult> ResetPasswordAsync(UserLoginModel user)
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
}
