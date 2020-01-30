using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Core.Events;
using ptPKT.Core.Entities;
using ptPKT.Core.Events;
using Xunit;

namespace ptPKT.Tests.Handler
{
    public class ItemCompletedEmailNotificationHandlerHandle
    {
        [Fact]
        public void ThrowsExceptionGivenNullEventArgument()
        {
            var handler = new ItemCompletedEmailNotificationHandler();

            Exception ex = Assert.Throws<ArgumentNullException>(() => handler.Handle(null));
        }

        [Fact]
        public void DoesNothingGivenEventInstance()
        {
            var handler = new ItemCompletedEmailNotificationHandler();

            handler.Handle(new ToDoItemCompletedEvent(new ToDoItem()));
        }
    }
}
