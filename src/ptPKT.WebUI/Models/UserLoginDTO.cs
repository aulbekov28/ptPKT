using System.ComponentModel.DataAnnotations;

namespace ptPKT.WebUI.Models
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Email is not specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
