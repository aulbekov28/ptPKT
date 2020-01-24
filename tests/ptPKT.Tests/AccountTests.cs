using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ptPKT.Core.Identity;
using ptPKT.WebUI.Controllers;
using ptPKT.WebUI.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ptPKT.Tests
{
    public class AccountTests
    {
        private readonly UserManager<AppUser> _userManager;
 
        public AccountTests()
        {
            Environment.SetEnvironmentVariable("JWT_SECRET", "verysecrettestjwtkeythatnoonewilleverknow");
            var userStoreMock = Mock.Of<IUserStore<AppUser>>();
            var userMgr = new Mock<UserManager<AppUser>>(userStoreMock, null, null, null, null, null, null, null, null);

            var fakeAppUser = new AppUserBuilder().Build();

            var user = new AppUser
            {
                UserName = fakeAppUser.UserName,
                FirstName = fakeAppUser.FirstName,
                SecondName = fakeAppUser.SecondName,
                Email = fakeAppUser.Email
            };

            var newFakeAppUserAppUser = new AppUserBuilder().NewUserRegistration().Build();

            var newUser = new AppUser
            {
                UserName = newFakeAppUserAppUser.UserName,
                FirstName = newFakeAppUserAppUser.FirstName,
                SecondName = newFakeAppUserAppUser.SecondName,
                Email = newFakeAppUserAppUser.Email
            };

            var tcs = new TaskCompletionSource<AppUser>();
            tcs.SetResult(user);

            userMgr.Setup(x => x.FindByEmailAsync(user.Email)).Returns(tcs.Task);
            userMgr.Setup(x => x.CheckPasswordAsync(user, fakeAppUser.Password)).Returns(Task.FromResult(true));
            //userMgr.Setup(x => x.CreateAsync(newUser, newFakeAppUserAppUser.Password)).Returns(Task.FromResult(IdentityResult.Success));
            userMgr.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));
            //userMgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            
            _userManager = userMgr.Object;
        }

        [Fact]
        public async Task UserLogsIn_ValidCredentials_ReturnsOkRequestResult()
        {
            var user = new AppUserBuilder().Build();
            var loginModel = new LoginModelDTO
            {
                Email = user.Email,
                Password = user.Password
            };

            var controller = new AccountController(_userManager);

            var result = await controller.SignIn(loginModel);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UserLogsIn_BadPassword_ReturnsBadRequestResult()
        {
            var user = new AppUserBuilder().WithWrongPassword().Build();
            var loginModel = new LoginModelDTO
            {
                Email = user.Email,
                Password = user.Password
            };

            var controller = new AccountController(_userManager);

            var result = await controller.SignIn(loginModel);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UserLogsIn_NotRegistered_ReturnsBadRequestResult()
        {
            var user = new AppUserBuilder().NotRegistered().Build();
            var loginModel = new LoginModelDTO
            {
                Email = user.Email,
                Password = user.Password
            };

            var controller = new AccountController(_userManager);

            var result = await controller.SignIn(loginModel);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UserSingUp_ValidNewUser_ReturnsOkResponse()
        {
            var user = new AppUserBuilder().NewUserRegistration().Build();
            var registerModel = new RegisterModelDTO()
            {
                Email = user.Email,
                Password = user.Password,
                UserName = user.UserName,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
            };

            var controller = new AccountController(_userManager);

            var result = await controller.SignUp(registerModel);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
