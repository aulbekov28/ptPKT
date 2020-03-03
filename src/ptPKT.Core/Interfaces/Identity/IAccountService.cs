using System.Collections.Generic;
using System.Threading.Tasks;
using ptPKT.Core.Identity;
using ptPKT.Core.Services.Identity;

namespace ptPKT.Core.Interfaces.Identity
{
    public interface IAccountService
    {
        Task ConfirmEmailAsync(string confirmationToken);
        Task ForgotPasswordAsync(UserLoginModel user);
        IEnumerable<AppUser> GetUsers();
        Task<UserLoginResult> ResetPasswordAsync(UserLoginModel user);
        Task<UserLoginResult> SignIn(UserLoginModel model);
        Task SignOut();
        Task<UserLoginResult> SignUp(UserRegistedModel model);
    }
}