using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ptPKT.Core.Entities.Identity
{
    public class AppUser : IdentityUser<int>
    {
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string SecondName { get; set; }
    }
}
