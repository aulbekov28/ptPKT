using System;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Identity;
using Moq;
using ptPKT.Core.Entities.Identity;
using ptPKT.Core.Entities.Infrastructure;
using ptPKT.Core.Interfaces;

namespace ptPKT.Tests
{
    public class MockHelper
    {
        public static Mock<UserManager<TUser>> UserManagerPasswordValidationMock<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());
            return mgr;
        }

        public static Mock<IEnvironmentContext> EnvironmentContextMock()
        {
            var environmentContext = new Mock<IEnvironmentContext>();

            var user = new AppUserBuilder().Build();

            environmentContext.Setup(x => x.GetCurrentUser()).Returns(user.GetAppUser());

            return environmentContext;
        }

        public static Mock<IEmailSender> EmailSenderMock()
        {
            var emailSender = new Mock<IEmailSender>();

            emailSender.Setup(x => x.Send(It.IsAny<EmailNotification>()));

            return emailSender;
        }

        public static Mock<UserManager<AppUser>> UserManagerMock()
        {
            var userStoreMock = Mock.Of<IUserStore<AppUser>>();
            var userMgr = new Mock<UserManager<AppUser>>(userStoreMock, null, null, null, null, null, null, null, null);

            var defaultAppUser = new AppUserBuilder().Build();

            var user = defaultAppUser.GetAppUser();

            var tcs = new TaskCompletionSource<AppUser>();
            tcs.SetResult(user);

            userMgr.Setup(x => x.FindByEmailAsync(user.Email)).Returns(tcs.Task);
            userMgr.Setup(x => x.CheckPasswordAsync(user, defaultAppUser.Password)).Returns(Task.FromResult(true));
            userMgr.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));

            //TODO https://stackoverflow.com/questions/49165810/how-to-mock-usermanager-in-net-core-testing/49174248

            return userMgr;
        }
    }
}
