using Yana.Application.Contracts.AuthenticationService;
using Yana.Application.Contracts.TokenService;

namespace Yana.Application.Features.Auth.Query.RefreshGoogleToken;

public class RefreshGoogleTokenQueryHandler : IRequestHandler<RefreshGoogleTokenQuery, Response<AccessTokenVm>>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ITokenService _tokenService;

    public RefreshGoogleTokenQueryHandler(IAuthenticationService authenticationService, ITokenService tokenService)
    {
        _authenticationService = authenticationService;
        _tokenService = tokenService;
    }

    public async Task<Response<AccessTokenVm>> Handle(RefreshGoogleTokenQuery request,
        CancellationToken cancellationToken)
    {
        var refreshToken = await _tokenService.GetRefreshToken(request.UserId, AuthProvider.Google);
        if (refreshToken is null)
        {
            return new Response<AccessTokenVm>
            {
                ErrorCode = ErrorCode.Unauthorized,
                ErrorMessage = $"Could not find refresh token provided by {AuthProvider.Google.Humanize()}"
            };
        }

        var refreshedAccessToken = await _authenticationService.RefreshAccessToken(request.UserId, refreshToken);
        return refreshedAccessToken is not null
            ? new Response<AccessTokenVm>
            {
                Result = new AccessTokenVm(refreshedAccessToken)
            }
            : new Response<AccessTokenVm>
            {
                ErrorCode = ErrorCode.Unauthorized,
                ErrorMessage = "Failed to fetch new access token"
            };
    }
}