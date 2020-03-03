using System.Collections.Generic;
using System.Threading.Tasks;
using ptPKT.Core.Identity;
using ptPKT.Core.Services.Account;

namespace ptPKT.Core.Interfaces.Account
{
    public interface IAccountService
    {
        Task ConfirmEmailAsync(string confirmationToken);
        Task ForgotPasswordAsync(UserLoginModel user);
        IEnumerable<AppUser> GetUsers();
        Task<LoginResult> ResetPasswordAsync(UserLoginModel user);
        Task<LoginResult> SignIn(UserLoginModel model);
        Task SignOut();
        Task<LoginResult> SignUp(UserRegistedModel model);
    }
}