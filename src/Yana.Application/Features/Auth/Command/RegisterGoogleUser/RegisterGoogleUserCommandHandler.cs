using Yana.Application.Contracts.AuthenticationService;
using Yana.Application.Contracts.TokenService;
using AuthenticationVm = Yana.Application.ViewModels.AuthenticationVm;

namespace Yana.Application.Features.Auth.Command.RegisterGoogleUser;

public sealed class RegisterGoogleUserCommandHandler
    : IRequestHandler<RegisterGoogleUserCommand, CommandResponse<AuthenticationVm>>
{
    private readonly IUserRepositoryService _userRepositoryService;
    private readonly ITokenService _tokenService;
    private readonly IAuthenticationService _authenticationService;

    public RegisterGoogleUserCommandHandler(IUserRepositoryService userRepositoryService, ITokenService tokenService,
        IAuthenticationService authenticationService)
    {
        _userRepositoryService = userRepositoryService;
        _tokenService = tokenService;
        _authenticationService = authenticationService;
    }

    public async Task<CommandResponse<AuthenticationVm>> Handle(RegisterGoogleUserCommand request,
        CancellationToken cancellationToken)
    {
        var (idToken, refreshToken) = await _authenticationService.GetUserTokens(request.Dto.AuthorizationCode);
        if (idToken is null || refreshToken is null)
        {
            return new CommandResponse<AuthenticationVm>()
            {
                ErrorCode = ErrorCode.Unauthorized,
                ErrorMessage = "Failed to get user tokens with provided authorization code"
            };
        }

        var userDto = _tokenService.GetUserInformationFromIdToken(idToken);
        if (userDto is null)
        {
            return new CommandResponse<AuthenticationVm>()
            {
                ErrorCode = ErrorCode.Unauthorized,
                ErrorMessage = "Failed to get user information from id token"
            };
        }

        await _userRepositoryService.UpsertUser(userDto);
        var userId = await _tokenService.InsertRefreshToken(userDto.ExternalId, refreshToken, AuthProvider.Google);

        if (userId is null)
        {
            return new CommandResponse<AuthenticationVm>()
            {
                ErrorCode = ErrorCode.Unauthorized,
                ErrorMessage = "Failed to create a user session"
            };
        }

        return new CommandResponse<AuthenticationVm>
        {
            Result = new AuthenticationVm(userId, idToken)
        };
    }
}