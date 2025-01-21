namespace Yana.Application.Features.Auth.Command.RegisterGoogleUser;

public sealed class RegisterGoogleUserCommandHandler
    : IRequestHandler<RegisterGoogleUserCommand, CommandResponse<UserVm>>
{
    private readonly IUserRepositoryService _userRepositoryService;

    public RegisterGoogleUserCommandHandler(IUserRepositoryService userRepositoryService)
    {
        _userRepositoryService = userRepositoryService;
    }

    public async Task<CommandResponse<UserVm>> Handle(RegisterGoogleUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepositoryService.UpsertUser(new UserProfileDto(
            ExternalId: request.User.ExternalUserProfiles.First(x => x.AuthProvider == AuthProvider.Google).Id,
            Email: request.User.Email,
            FirstName: request.User.FirstName,
            LastName: request.User.LastName,
            AuthProvider: AuthProvider.Google
        ));

        var isRefreshTokenUpdated = await _userRepositoryService.UpsertRefreshToken(user, AuthProvider.Google);
        if (!isRefreshTokenUpdated)
        {
            return new CommandResponse<UserVm>
            {
                ErrorCode = ErrorCode.Unauthorized,
                ErrorMessage = "Failed to create user session"
            };
        }


        return new CommandResponse<UserVm>
        {
            Result = new UserVm
            {
                UserId = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedDate = user.CreatedDate
            }
        };
    }
}