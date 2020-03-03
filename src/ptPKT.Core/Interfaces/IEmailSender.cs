using System;
using System.Collections.Generic;
using System.Text;
using ptPKT.Core.Entities.Infrastructure;

namespace ptPKT.Core.Interfaces
{
    public interface IEmailSender
    {
        void Send(EmailNotification emailNotification);
    }
}
