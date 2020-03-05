using System;
using System.ComponentModel.DataAnnotations;

namespace ptPKT.WebUI.Models
{
    [Obsolete]
    public class UserLoginDto
    {
        [Required(ErrorMessage = "NewEmail is not specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
