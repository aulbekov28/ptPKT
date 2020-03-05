using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ptPKT.WebUI.Controllers;
using ptPKT.WebUI.Models;
using System;
using System.Threading.Tasks;
using ptPKT.Core.Entities.Identity;
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
            var userStoreMock = Mock.Of<IUserStore<AppUser>>();
            var userMgr = new Mock<UserManager<AppUser>>(userStoreMock, null, null, null, null, null, null, null, null);

            var defaultAppUser = new AppUserBuilder().Build();

            var user = defaultAppUser.GetAppUser();

            var newFakeAppUserAppUser = new AppUserBuilder().NewUserRegistration().Build();

            var tcs = new TaskCompletionSource<AppUser>();
            tcs.SetResult(user);

            userMgr.Setup(x => x.FindByEmailAsync(user.Email)).Returns(tcs.Task);
            userMgr.Setup(x => x.CheckPasswordAsync(user, defaultAppUser.Password)).Returns(Task.FromResult(true));
            userMgr.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));

            //TODO https://stackoverflow.com/questions/49165810/how-to-mock-usermanager-in-net-core-testing/49174248

            var userManager = userMgr.Object;
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

            var controller = new AccountController(_identityService);

            var result = await controller.SignIn(loginModel);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UserLogsIn_BadPassword_ReturnsBadRequestResult()
        {
            var user = new AppUserBuilder().WithWrongPassword().Build();
            var loginModel = new UserLoginModel()
            {
                Email = user.Email,
                Password = user.Password
            };

            var controller = new AccountController(_identityService);

            var result = await controller.SignIn(loginModel);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UserLogsIn_NotRegistered_ReturnsBadRequestResult()
        {
            var user = new AppUserBuilder().NotRegistered().Build();
            var loginModel = new UserLoginModel()
            {
                Email = user.Email,
                Password = user.Password
            };

            var controller = new AccountController(_identityService);

            var result = await controller.SignIn(loginModel);
            Assert.IsType<BadRequestObjectResult>(result);
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

            var controller = new AccountController(_identityService);

            var result = await controller.SignUp(registerModel);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
