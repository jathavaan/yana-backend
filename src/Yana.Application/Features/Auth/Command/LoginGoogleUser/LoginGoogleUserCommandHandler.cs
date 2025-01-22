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
        if (idToken is null || refreshToken is null)
        {
            return new CommandResponse<AuthenticationVm>
            {
                ErrorCode = ErrorCode.Unauthorized,
                ErrorMessage = "Failed to get user tokens"
            };
        }


        var userDto = _tokenService.GetUserInformationFromIdToken(idToken);
        if (userDto is null)
        {
            return new CommandResponse<AuthenticationVm>()
            {
                ErrorCode = ErrorCode.Unauthorized,
                ErrorMessage = "Failed to construct a user object from ID token"
            };
        }

        var userId = await _tokenService.InsertRefreshToken(userDto.ExternalId, refreshToken, AuthProvider.Google);
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