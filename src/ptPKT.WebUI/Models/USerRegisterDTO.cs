using System;
using System.ComponentModel.DataAnnotations;

namespace ptPKT.WebUI.Models
{
    [Obsolete]
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "NewEmail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string SecondName { get; set; }
    }
}
