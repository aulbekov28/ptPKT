namespace ptPKT.Core.Services.Identity
{
    public class UserLoginResult
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}