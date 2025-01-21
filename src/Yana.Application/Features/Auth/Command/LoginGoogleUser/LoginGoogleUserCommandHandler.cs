using Yana.Application.Contracts.AuthenticationService;
using Yana.Application.Contracts.TokenService;

namespace Yana.Application.Features.Auth.Command.LoginGoogleUser;

public class LoginGoogleUserCommandHandler : IRequestHandler<LoginGoogleUserCommand, CommandResponse<bool>>
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepositoryService _userRepositoryService;
    private readonly IAuthenticationService _authenticationService;

    public LoginGoogleUserCommandHandler(ITokenService tokenService, IUserRepositoryService userRepositoryService,
        IAuthenticationService authenticationService)
    {
        _tokenService = tokenService;
        _userRepositoryService = userRepositoryService;
        _authenticationService = authenticationService;
    }

    public async Task<CommandResponse<bool>> Handle(LoginGoogleUserCommand request, CancellationToken cancellationToken)
    {
        var (idToken, refreshToken) = await _authenticationService.GetUserTokens(request.Dto.AuthorizationCode);

        var result = true;

        if (!result)
        {
            return new CommandResponse<bool>
            {
                ErrorCode = ErrorCode.NotFound,
                ErrorMessage =
                    $"Instance {AuthProvider.Google.Humanize()} authentication have not been registered for the user"
            };
        }

        return new CommandResponse<bool>
        {
            Result = result
        };
    }
}