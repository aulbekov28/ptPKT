using System;
using System.Collections.Generic;
using System.Text;

namespace ptPKT.Core.Entities.Infrastrucutre
{
    public class EmailNotification
    {
        public string To { get; set; }
        public string From { get; set; }
        public string ReplyTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
