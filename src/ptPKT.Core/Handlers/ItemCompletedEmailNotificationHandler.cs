using System;
using ptPKT.Core.Entities.Infrastructure;
using ptPKT.Core.Events;
using ptPKT.Core.Interfaces;
using ptPKT.SharedKernel.Interfaces;

namespace ptPKT.Core.Handlers
{
    public class ItemCompletedEmailNotificationHandler : IHandle<ToDoItemCompletedEvent>
    {
        private readonly IEmailSender _sender;

        public ItemCompletedEmailNotificationHandler(IEmailSender sender)
        {
            _sender = sender;
        }

        public void Handle(ToDoItemCompletedEvent domainEvent)
        {
            if (domainEvent is null)
            {
                throw new ArgumentNullException();
            }

            var emailNotification = new EmailNotification()
            {
                To = "",
                From = "",
                Subject = $"{domainEvent.CompletedItem.Title} - is completed",
                Body = $"Hello! Your ToDo item {domainEvent.CompletedItem.Title} was completed at {domainEvent.DateOccurred}. /r/n {domainEvent.CompletedItem.Description}"
            };

            _sender.Send(emailNotification);
            Console.WriteLine($"{typeof(ItemCompletedEmailNotificationHandler)} occured");
        }
    }
}