using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using ptPKT.Core.Entities.Infrastrucutre;
using ptPKT.Core.Interfaces;

namespace ptPKT.Core.Services
{
    public class EmailSender : IEmailSender
    {
        public void Send(EmailNotification emailNotification)
        {
            using (var mail = new MailMessage())
            {
                mail.To.Add(emailNotification.To);
                mail.From = new MailAddress(emailNotification.From);
                mail.Subject = emailNotification.Subject;
                mail.Body = emailNotification.Body;
                mail.IsBodyHtml = true;

                var client = new SmtpClient();
                client.Send(mail);
            }
        }
    }
}
