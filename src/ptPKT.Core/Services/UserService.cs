using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ptPKT.Core.Entities.Identity;

namespace ptPKT.Core.Interfaces
{
    public class UserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public AppUser GetUserById(int id)
        {
            return _userManager.Users.Single(x => x.Id == id);
        }

        public async Task<AppUser> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public IEnumerable<AppUser> GetUsers()
        {
            return _userManager.Users;
        }
    }
}
