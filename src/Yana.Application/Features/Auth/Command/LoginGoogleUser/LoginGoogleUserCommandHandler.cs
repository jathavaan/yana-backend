using Yana.Application.Contracts.AuthenticationService;
using Yana.Application.Contracts.TokenService;

namespace Yana.Application.Features.Auth.Command.LoginGoogleUser;

public class LoginGoogleUserCommandHandler : IRequestHandler<LoginGoogleUserCommand, CommandResponse<AuthenticationVm>>
{
    private readonly ITokenService _tokenService;
    private readonly IAuthenticationService _authenticationService;

    public LoginGoogleUserCommandHandler(ITokenService tokenService, IAuthenticationService authenticationService)
    {
        _tokenService = tokenService;
        _authenticationService = authenticationService;
    }

    public async Task<CommandResponse<AuthenticationVm>> Handle(LoginGoogleUserCommand request,
        CancellationToken cancellationToken)
    {
        var (idToken, refreshToken) = await _authenticationService.GetUserTokens(request.Dto.AuthorizationCode);
        var externalId = _tokenService.GetExternalIdFromIdToken(idToken);
        var userId = await _tokenService.InsertRefreshToken(externalId, refreshToken, AuthProvider.Google);

        if (userId is null)
        {
            return new CommandResponse<AuthenticationVm>
            {
                ErrorCode = ErrorCode.NotFound,
                ErrorMessage =
                    $"Instance {AuthProvider.Google.Humanize()} authentication have not been registered for the user"
            };
        }

        return new CommandResponse<AuthenticationVm>
        {
            Result = new AuthenticationVm(userId, idToken)
        };
    }
}