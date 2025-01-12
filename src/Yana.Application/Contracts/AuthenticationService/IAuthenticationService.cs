namespace Yana.Application.Contracts.AuthenticationService;

public interface IAuthenticationService
{
    public Task<string?> RefreshAccessToken(string userId, string refreshToken);
}