namespace ptPKT.Core.Entities.Infrastructure
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
