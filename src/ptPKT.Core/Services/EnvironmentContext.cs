using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ptPKT.Core.Entities.Identity;
using ptPKT.Core.Interfaces;

namespace ptPKT.Core.Services
{
    public class EnvironmentContext : IEnvironmentContext
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private AppUser _currentUser;

        public EnvironmentContext(UserManager<AppUser> userManager,
                                  IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public AppUser GetCurrentUser()
        {
            var userId = GetUserIdClaim().Claims.First(u => u.Type == "id").Value;
            if (_currentUser != null)
            {
                _currentUser = _userManager.FindByIdAsync(userId).Result;
            }
            return _currentUser;
        }

        private ClaimsPrincipal GetUserIdClaim()
        {
            var claimsPrincipal = _contextAccessor.HttpContext.User;

            if (!claimsPrincipal.Identity.IsAuthenticated)
                throw new AuthenticationException();

            return claimsPrincipal;
        }
    }
}
