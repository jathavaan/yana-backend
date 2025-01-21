using Yana.Application.Contracts.AuthenticationService;
using Yana.Application.Contracts.TokenService;

namespace Yana.Application.Features.Auth.Query.RefreshGoogleToken;

public class RefreshGoogleTokenQueryHandler : IRequestHandler<RefreshGoogleTokenQuery, Response<AccessTokenVm>>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ITokenService _tokenService;
    private readonly IUserRepositoryService _userRepositoryService;

    public RefreshGoogleTokenQueryHandler(IAuthenticationService authenticationService, ITokenService tokenService,
        IUserRepositoryService userRepositoryService)
    {
        _authenticationService = authenticationService;
        _tokenService = tokenService;
        _userRepositoryService = userRepositoryService;
    }

    public async Task<Response<AccessTokenVm>> Handle(RefreshGoogleTokenQuery request,
        CancellationToken cancellationToken)
    {
        var externalId = await _userRepositoryService.GetExternalIdById(request.UserId, AuthProvider.Google);

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