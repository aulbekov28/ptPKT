using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System;
using ptPKT.Core.Identity;
using ptPKT.WebUI.Controllers;
using ptPKT.WebUI.Models;

namespace ptPKT.Tests
{
    public class AccountTests
    {
        private UserManager<AppUser> userManager;
        private const string passwd = "password";
        private const string email = "test@test.mail";

        [SetUp]
        public void Setup()
        {
            Environment.SetEnvironmentVariable("JWT_SECRET", "verysecrettestjwtkeythatnoonewilleverknow");
            var UserStoreMock = Mock.Of<IUserStore<AppUser>>();
            var userMgr = new Mock<UserManager<AppUser>>(UserStoreMock, null, null, null, null, null, null, null, null);
            var user = new AppUser
            {
                // UserName = "test",
                FirstName = "test",
                SecondName = "test",
                // Email = email
            };


            var tcs = new TaskCompletionSource<AppUser>();
            tcs.SetResult(user);

            //userMgr.Setup(x => x.FindByEmailAsync(user.Email)).Returns(tcs.Task);
            userMgr.Setup(x => x.CheckPasswordAsync(user, "password")).Returns(Task.FromResult(true));

            userManager = userMgr.Object;
        }
        [Test]
        public async Task UserLogsIn_WithValidCredentials()
        {
            var loginModel = new LoginModel()
            {
                Email = email,
                Password = "password"
            };

            var controller = new AccountController(userManager);

            var result = await controller.SignIn(loginModel);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public async Task UserLogsIn_WithBadPassword()
        {
            var loginModel = new LoginModel()
            {
                Email = email,
                Password = "badpassword"
            };

            var controller = new AccountController(userManager);

            var result = await controller.SignIn(loginModel);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);
        }
    }
}
