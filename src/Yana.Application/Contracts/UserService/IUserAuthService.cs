using System.Security.Claims;

namespace Yana.Application.Contracts.UserService;

public interface IUserAuthService
{
    public UserProfileDto GetUserFromPrincipal(ClaimsPrincipal principal);
}