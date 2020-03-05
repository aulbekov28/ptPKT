using System;
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
        public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());
            return mgr;
        }

        public static Mock<IEnvironmentContext> MockEnvironmentContext()
        {
            var mockEnvironmentContext = new Mock<IEnvironmentContext>();

            mockEnvironmentContext.Setup(x => x.GetCurrentUser()).Returns(new AppUser()
            {
                Id = 1,
                Email = string.Empty,
                UserName = string.Empty
            });

            return mockEnvironmentContext;
        }

        public static Mock<IEmailSender> MockEmailSender()
        {
            var mockEmailSender = new Mock<IEmailSender>();

            mockEmailSender.Setup(x => x.Send(It.IsAny<EmailNotification>()));

            return mockEmailSender;
        }
    }
}
