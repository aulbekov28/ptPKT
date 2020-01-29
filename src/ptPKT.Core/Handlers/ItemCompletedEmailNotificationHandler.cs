using System;
using ptPKT.Core.Events;
using ptPKT.SharedKernel.Interfaces;

namespace CleanArchitecture.Core.Events
{
    public class ItemCompletedEmailNotificationHandler : IHandle<ToDoItemCompletedEvent>
    {
        public void Handle(ToDoItemCompletedEvent domainEvent)
        {
            // Do Something
            Console.WriteLine("123");
        }
    }
}