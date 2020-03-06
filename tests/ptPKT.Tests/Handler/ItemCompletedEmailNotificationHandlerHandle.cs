using ptPKT.Core.Entities.BL;
using ptPKT.Core.Events;
using ptPKT.Core.Handlers;
using System;
using ptPKT.Core.Interfaces;
using Xunit;

namespace ptPKT.Tests.Handler
{
    public class ItemCompletedEmailNotificationHandlerHandle
    {
        private readonly IEmailSender _emailSender;

        public ItemCompletedEmailNotificationHandlerHandle()
        {
            _emailSender = MockHelper.EmailSenderMock().Object;
        }


        [Fact]
        public void ThrowsExceptionGivenNullEventArgument()
        {
            var handler = new ItemCompletedEmailNotificationHandler(_emailSender);

            Exception ex = Assert.Throws<ArgumentNullException>(() => handler.Handle(null));
        }

        [Fact]
        public void DoesNothingGivenEventInstance()
        {
            var handler = new ItemCompletedEmailNotificationHandler(_emailSender);

            handler.Handle(new ToDoItemCompletedEvent(new ToDoItem()));
        }
    }
}
