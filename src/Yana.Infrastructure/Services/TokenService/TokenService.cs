using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Yana.Infrastructure.Services.TokenService;

public class TokenService : ITokenService
{
    private readonly YanaDbContext _dbContext;
    private readonly IUserRepositoryService _userRepositoryService;
    private readonly IEncryptionService _encryptionService;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly IOptionsMonitor<GoogleAuthenticationOptions> _googleAuthenticationOptions;

    public TokenService(YanaDbContext dbContext, IUserRepositoryService userRepositoryService,
        IEncryptionService encryptionService, JwtSecurityTokenHandler tokenHandler,
        IOptionsMonitor<GoogleAuthenticationOptions> googleAuthenticationOptions)
    {
        _dbContext = dbContext;
        _userRepositoryService = userRepositoryService;
        _encryptionService = encryptionService;
        _tokenHandler = tokenHandler;
        _googleAuthenticationOptions = googleAuthenticationOptions;
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

    public UserProfileDto? GetUserInformationFromIdToken(string idToken)
    {
        if (!_tokenHandler.CanReadToken(idToken)) return null;
        var token = (JwtSecurityToken)_tokenHandler.ReadToken(idToken);

        foreach (var claim in token.Claims)
        {
            Console.WriteLine($"{claim.Type}, {claim.Value}");
        }

        var identity = new ClaimsIdentity(token.Claims.Select(c =>
        {
            return c.Type switch
            {
                "sub" => new Claim(ClaimTypes.NameIdentifier, c.Value),
                "email" => new Claim(ClaimTypes.Email, c.Value),
                "given_name" => new Claim(ClaimTypes.GivenName, c.Value),
                "family_name" => new Claim(ClaimTypes.Surname, c.Value),
                _ => new Claim(c.Type, c.Value)
            };
        }));

        var claimsPrincipal = new ClaimsPrincipal(identity);
        return GetUserFromPrincipal(claimsPrincipal);
    }

    public UserProfileDto GetUserFromPrincipal(ClaimsPrincipal principal)
        => new(
            ExternalId: principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString(),
            Email: principal.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty,
            FirstName: principal.FindFirst(ClaimTypes.GivenName)?.Value ?? string.Empty,
            LastName: principal.FindFirst(ClaimTypes.Surname)?.Value ?? string.Empty,
            AuthProvider: ResolveAuthProvider(
                principal.Claims
                    .FirstOrDefault(c => c.Type == "iss")?.Value ?? string.Empty
            )
        );

    private AuthProvider ResolveAuthProvider(string issuer)
        => issuer switch
        {
            _ when string.Equals(issuer, _googleAuthenticationOptions.CurrentValue.Issuer,
                    StringComparison.OrdinalIgnoreCase)
                => AuthProvider.Google,
            _ => throw new Exception("Invalid issuer")
        };
}