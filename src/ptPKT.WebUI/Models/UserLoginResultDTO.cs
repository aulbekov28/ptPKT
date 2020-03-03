using System;

namespace ptPKT.WebUI.Models
{
    [Obsolete]
    public class UserLoginResultDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public string FistName { get; set; }

        public string SecondName { get; set; }
    }
}
