using ptPKT.Core.Entities.Identity;

namespace ptPKT.Core.Interfaces
{
    public interface IEnvironmentService
    {
        AppUser GetCurrentUser();
    }
}