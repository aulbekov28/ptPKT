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
        private const string GoodPasswd = "badpassword";
        private const string BadPasswd = "password";
        private const string Email = "test@test.mail";

        public AccountTests()
        {
            Environment.SetEnvironmentVariable("JWT_SECRET", "verysecrettestjwtkeythatnoonewilleverknow");
            var userStoreMock = Mock.Of<IUserStore<AppUser>>();
            var userMgr = new Mock<UserManager<AppUser>>(userStoreMock, null, null, null, null, null, null, null, null);
            var user = new AppUser
            {
                UserName = "test",
                FirstName = "test",
                SecondName = "test",
                Email = Email
            };

            var tcs = new TaskCompletionSource<AppUser>();
            tcs.SetResult(user);

            userMgr.Setup(x => x.FindByEmailAsync(user.Email)).Returns(tcs.Task);
            userMgr.Setup(x => x.CheckPasswordAsync(user, GoodPasswd)).Returns(Task.FromResult(true));

            _userManager = userMgr.Object;
        }

        [Fact]
        public async Task UserLogsIn_WithValidCredentials()
        {
            var loginModel = new LoginModel
            {
                Email = Email,
                Password = GoodPasswd
            };

            var controller = new AccountController(_userManager);

            var result = await controller.SignIn(loginModel);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UserLogsIn_WithBadPassword()
        {
            var loginModel = new LoginModel
            {
                Email = Email,
                Password = BadPasswd
            };

            var controller = new AccountController(_userManager);

            var result = await controller.SignIn(loginModel);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
