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
        var user = await _userRepositoryService.UpsertUser(
            new UserProfileDto(
                ExternalId: request.User.ExternalUserProfiles.First(x => x.AuthProvider == AuthProvider.Google).Id,
                Email: request.User.Email,
                FirstName: request.User.FirstName,
                LastName: request.User.LastName,
                AuthProvider: AuthProvider.Google
            ),
            request.RefreshToken
        );

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