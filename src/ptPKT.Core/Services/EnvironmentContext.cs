using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ptPKT.Core.Identity;

namespace ptPKT.Core.Services
{
    public class EnvironmentContext
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public EnvironmentContext(UserManager<AppUser> userManager,
                                  IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public AppUser GetCurrentUser()
        {
            var userId = GetUserIdClaim().Claims.First(u => u.Type == "id").Value;
            return _userManager.FindByIdAsync(userId).Result;
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
