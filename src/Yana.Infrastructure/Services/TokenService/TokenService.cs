using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Yana.Infrastructure.Services.TokenService;

public class TokenService : ITokenService
{
    private readonly YanaDbContext _dbContext;
    private readonly IUserRepositoryService _userRepositoryService;
    private readonly IEncryptionService _encryptionService;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public TokenService(YanaDbContext dbContext, IUserRepositoryService userRepositoryService,
        IEncryptionService encryptionService, JwtSecurityTokenHandler tokenHandler)
    {
        _dbContext = dbContext;
        _userRepositoryService = userRepositoryService;
        _encryptionService = encryptionService;
        _tokenHandler = tokenHandler;
    }

    public async Task<string?> GetRefreshToken(string userId, AuthProvider authProvider)
    {
        var encryptedRefreshToken =
            (await _userRepositoryService.GetExternalUserProfile(userId, authProvider))?.RefreshToken;
        return encryptedRefreshToken is null ? null : _encryptionService.Decrypt(encryptedRefreshToken);
    }

    public async Task<string?> InsertRefreshToken(string externalUserId, string refreshToken, AuthProvider authProvider)
    {
        var userProfile = await _userRepositoryService.GetUserByExternalId(externalUserId);
        var externalUserProfile = userProfile?.ExternalUserProfiles.FirstOrDefault(x => x.AuthProvider == authProvider);
        if (externalUserProfile is null) return null;
        var encryptedRefreshToken = _encryptionService.Encrypt(refreshToken);

        externalUserProfile.RefreshToken = encryptedRefreshToken;
        _dbContext.ExternalUserProfiles.Update(externalUserProfile);
        await _dbContext.SaveChangesAsync();

        return userProfile.Id;
    }

    public async Task<bool> InvalidateRefreshToken(string userId, AuthProvider authProvider)
    {
        var externalUserProfile = await _userRepositoryService.GetExternalUserProfile(userId, authProvider);
        if (externalUserProfile is null) return false;

        externalUserProfile.RefreshToken = null;
        _dbContext.ExternalUserProfiles.Update(externalUserProfile);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public string? GetExternalIdFromIdToken(string idToken)
    {
        if (!_tokenHandler.CanReadToken(idToken)) return null;
        var token = (JwtSecurityToken)_tokenHandler.ReadToken(idToken);
        var externalId = token.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        return externalId;
    }
}