using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ptPKT.WebUI.Controllers;
using ptPKT.WebUI.Models;
using System;
using System.Threading.Tasks;
using ptPKT.Core.Entities.Identity;
using ptPKT.Core.Exceptions.Indentity;
using ptPKT.Core.Interfaces.Identity;
using ptPKT.Core.Services.Identity;
using Xunit;

namespace ptPKT.Tests
{
    public class AccountTests
    {
        private readonly IIdentityService _identityService;
 
        public AccountTests()
        {
            Environment.SetEnvironmentVariable("JWT_SECRET", "verysecrettestjwtkeythatnoonewilleverknow");

            var userManager = MockHelper.UserManagerMock().Object;

            _identityService = new IdentityService(userManager);
        }

        [Fact]
        public async Task UserLogsIn_ValidCredentials_ReturnsOkRequestResult()
        {
            var user = new AppUserBuilder().Build();
            var loginModel = new UserLoginModel()
            {
                Email = user.Email,
                Password = user.Password
            };

            var controller = new AuthController(_identityService);

            var result = await controller.SignIn(loginModel);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void UserLogsIn_BadPassword_ThrowsException()
        {
            var user = new AppUserBuilder().WithWrongPassword().Build();
            var loginModel = new UserLoginModel()
            {
                Email = user.Email,
                Password = user.Password
            };

            var controller = new AuthController(_identityService);

            Assert.ThrowsAsync<IncorrectCredentialsException>(() => controller.SignIn(loginModel));
        }

        [Fact]
        public void UserLogsIn_NotRegistered_ReturnsBadRequestResult()
        {
            var user = new AppUserBuilder().NotRegistered().Build();
            var loginModel = new UserLoginModel()
            {
                Email = user.Email,
                Password = user.Password
            };

            var controller = new AuthController(_identityService);

            Assert.ThrowsAsync<UserNotFound>(() => controller.SignIn(loginModel));
            
            // var result = await controller.SignIn(loginModel);
            // Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UserSingUp_ValidNewUser_ReturnsOkResponse()
        {
            var user = new AppUserBuilder().NewUserRegistration().Build();
            var registerModel = new UserRegistedModel()
            {
                Email = user.Email,
                Password = user.Password,
                UserName = user.UserName,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
            };

            var controller = new AuthController(_identityService);

            var result = await controller.SignUp(registerModel);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
