using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ptPKT.Core.Entities.Identity;
using ptPKT.Core.Exceptions.Indentity;
using ptPKT.Core.Interfaces.Identity;

namespace ptPKT.Core.Services.Identity
{
    public partial class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;

        public IdentityService(UserManager<AppUser> userManager)
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
                throw new AppUserIdentityException(result.Errors);

            var response = new UserLoginResult()
            {
                Id = newUser.Id,
                FirstName = newUser.FirstName,
                SecondName = newUser.SecondName,
                Token = GenerateToken(newUser)
            };

            return response;
        }

        public UserLoginResult SignOut()
        {
            var nullUser = new NullUser();
            return new UserLoginResult()
            {
                Id = nullUser.Id,
                Token = GenerateToken(nullUser)
            };
        }

        public async Task ConfirmEmailAsync(int id,string confirmationToken)
        {
            var user = GetUserById(id);
            var result = await _userManager.ConfirmEmailAsync(user, confirmationToken);

            if (!result.Succeeded)
                throw new AppUserIdentityException(result.Errors);
        }

        public async Task<string> ForgotPasswordAsync(ForgotPasswordModel model)
        {
            var user = GetUserByEmail(model.Email);

            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            return passwordResetToken;
        }

        public async Task ResetPasswordAsync(ResetPasswordModel model)
        {
            var user = GetUserByEmail(model.Email);

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (!result.Succeeded)
                throw new AppUserIdentityException(result.Errors);
        }

        public async Task<UserLoginResult> ChangeEmailAsync(ChangeEmailModel model)
        {
            var user = GetUserById(model.Id);
            var result = await _userManager.ChangeEmailAsync(user, model.NewEmail, model.Token);
            
            if (!result.Succeeded)
                throw new AppUserIdentityException(result.Errors);

            var response = new UserLoginResult()
            {
                Id = user.Id,
                SecondName = user.SecondName,
                FirstName = user.FirstName,
                UserName = user.UserName,
                Token = GenerateToken(user)
            };

            return response;
        }

        private static string GenerateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim("name", user.UserName),
                new Claim("id", user.Id.ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")));

            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(60);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task ValidateUser(AppUser user)
        {
            if (user == null)
                throw new AppUserNotFoundException();

            if (!await _userManager.IsEmailConfirmedAsync(user))
                throw new EmailNotConfirmedException();

            var lockoutEndDate = await _userManager.GetLockoutEndDateAsync(user);
            if (lockoutEndDate.HasValue && lockoutEndDate.Value > DateTimeOffset.Now)
                throw new UserIsLockedException();
        }

        private AppUser GetUserByEmail(string email)
        {
            return _userManager.FindByEmailAsync(email).Result;
        }

        private AppUser GetUserById(int id)
        {
            return _userManager.Users.Single(x => x.Id == id);
        }
    }
}
