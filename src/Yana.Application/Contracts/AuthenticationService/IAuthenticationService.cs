namespace Yana.Application.Contracts.AuthenticationService;

public interface IAuthenticationService
{
    public Task<(string idToken, string refreshToken)> GetUserTokens(string authoirzationCode);
    public Task<string?> GetRefreshToken(string userId, string idToken);
    public Task<string?> RefreshAccessToken(string externalId, string refreshToken);
}