using System;
using ptPKT.Core.Events;
using ptPKT.SharedKernel.Interfaces;

namespace CleanArchitecture.Core.Events
{
    public class ItemCompletedEmailNotificationHandler : IHandle<ToDoItemCompletedEvent>
    {
        public void Handle(ToDoItemCompletedEvent domainEvent)
        {
            if (domainEvent is null)
            {
                throw new ArgumentNullException();
            }

            // Do Something
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine($"{typeof(ItemCompletedEmailNotificationHandler)} occured");
        }
    }
}