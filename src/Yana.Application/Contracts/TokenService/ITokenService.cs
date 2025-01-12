namespace Yana.Application.Contracts.TokenService;

public interface ITokenService
{
    public Task<string?> GetRefreshToken(string userId, AuthProvider authProvider);
    public Task<bool> InsertRefreshToken(string userId, string refreshToken, AuthProvider authProvider);
    public Task<bool> InvalidateRefreshToken(string userId, AuthProvider authProvider);
}