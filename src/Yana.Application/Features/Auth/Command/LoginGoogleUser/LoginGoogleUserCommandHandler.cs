using Yana.Application.Contracts.TokenService;

namespace Yana.Application.Features.Auth.Command.LoginGoogleUser;

public class LoginGoogleUserCommandHandler : IRequestHandler<LoginGoogleUserCommand, CommandResponse<bool>>
{
    private readonly ITokenService _tokenService;

    public LoginGoogleUserCommandHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<CommandResponse<bool>> Handle(LoginGoogleUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _tokenService.InsertRefreshToken(request.User.Id, request.RefreshToken, AuthProvider.Google);

        if (!result)
        {
            return new CommandResponse<bool>
            {
                ErrorCode = ErrorCode.NotFound,
                ErrorMessage =
                    $"Instance {AuthProvider.Google.Humanize()} authentication have not been registered for the user ${request.User.Id}"
            };
        }

        return new CommandResponse<bool>
        {
            Result = result
        };
    }
}