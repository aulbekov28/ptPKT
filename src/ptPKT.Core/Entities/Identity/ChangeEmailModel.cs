namespace ptPKT.Core.Entities.Identity
{
    public class ChangeEmailModel
    {
        public int Id { get; set; }
        public string NewEmail { get; set; }
        public string Token { get; set; }
    }
}
