using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using ptPKT.Core.Entities.Identity;

namespace ptPKT.Core.Interfaces
{
    public class UsersService
    {
        private readonly UserManager<AppUser> _userManager;

        public UsersService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public AppUser GetUserById(int id)
        {
            return _userManager.Users.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<AppUser> GetUsers()
        {
            return _userManager.Users;
        }
    }
}
