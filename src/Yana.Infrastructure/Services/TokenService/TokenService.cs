namespace Yana.Infrastructure.Services.TokenService;

public class TokenService : ITokenService
{
    private readonly YanaDbContext _dbContext;
    private readonly IUserRepositoryService _userRepositoryService;
    private readonly IEncryptionService _encryptionService;

    public TokenService(YanaDbContext dbContext, IUserRepositoryService userRepositoryService,
        IEncryptionService encryptionService)
    {
        _dbContext = dbContext;
        _userRepositoryService = userRepositoryService;
        _encryptionService = encryptionService;
    }

    public async Task<string?> GetRefreshToken(string userId, AuthProvider authProvider)
    {
        var encryptedRefreshToken =
            (await _userRepositoryService.GetExternalUserProfile(userId, authProvider))?.RefreshToken;
        return encryptedRefreshToken is null ? null : _encryptionService.Decrypt(encryptedRefreshToken);
    }

    public async Task<bool> InsertRefreshToken(string userId, string refreshToken, AuthProvider authProvider)
    {
        var externalUserProfile = await _userRepositoryService.GetExternalUserProfile(userId, authProvider);
        if (externalUserProfile is null) return false;
        var encryptedRefreshToken = _encryptionService.Encrypt(refreshToken);

        externalUserProfile.RefreshToken = encryptedRefreshToken;
        _dbContext.ExternalUserProfiles.Update(externalUserProfile);
        await _dbContext.SaveChangesAsync();

        return true;
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
}