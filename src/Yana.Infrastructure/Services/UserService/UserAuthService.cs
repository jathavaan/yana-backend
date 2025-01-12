using System.Security.Claims;
using Microsoft.Extensions.Options;
using Yana.Domain.Enums;

namespace Yana.Infrastructure.Services.UserService;

public sealed class UserAuthService : IUserAuthService
{
    private readonly IOptionsMonitor<GoogleAuthenticationOptions> _googleAuthenticationOptions;

    public UserAuthService(IOptionsMonitor<GoogleAuthenticationOptions> googleAuthenticationOptions)
    {
        _googleAuthenticationOptions = googleAuthenticationOptions;
    }

    public UserProfileDto GetUserFromPrincipal(ClaimsPrincipal principal)
        => new(
            ExternalId: principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString(),
            Email: principal.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty,
            FirstName: principal.FindFirst(ClaimTypes.GivenName)?.Value ?? string.Empty,
            LastName: principal.FindFirst(ClaimTypes.Surname)?.Value ?? string.Empty,
            AuthProvider: ResolveAuthProvider(principal.FindFirst(ClaimTypes.NameIdentifier)?.Issuer ?? string.Empty)
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