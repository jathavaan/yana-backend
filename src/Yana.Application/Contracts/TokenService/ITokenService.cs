namespace Yana.Application.Contracts.TokenService;

public interface ITokenService
{
    public Task<string?> GetRefreshToken(string userId, AuthProvider authProvider);
    public Task<string?> InsertRefreshToken(string externalUserId, string refreshToken, AuthProvider authProvider);
    public Task<bool> InvalidateRefreshToken(string userId, AuthProvider authProvider);
    public string? GetExternalIdFromIdToken(string idToken);
}