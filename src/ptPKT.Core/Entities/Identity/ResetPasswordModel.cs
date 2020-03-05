namespace ptPKT.Core.Services.Identity
{
    public class ResetPasswordModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
