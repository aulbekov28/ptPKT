using System.Collections.Generic;
using System.Threading.Tasks;
using ptPKT.Core.Entities.Identity;
using ptPKT.Core.Services.Identity;

namespace ptPKT.Core.Interfaces.Identity
{
    public interface IIdentityService
    {
        Task<UserLoginResult> ChangeEmailAsync(ChangeEmailModel model);
        Task ConfirmEmailAsync(int id, string confirmationToken);
        Task<string> ForgotPasswordAsync(ForgotPasswordModel model);
        Task ResetPasswordAsync(ResetPasswordModel model);
        Task<UserLoginResult> SignIn(UserLoginModel model);
        UserLoginResult SignOut();
        Task<UserLoginResult> SignUp(UserRegistedModel model);
    }
}