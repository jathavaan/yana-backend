namespace Yana.Application.Features.Auth.Command.RegisterGoogleUser;

public sealed class RegisterGoogleUserCommand(UserProfile user) : Command<CommandResponse<UserVm>>
{
    public UserProfile User { get; } = user;
}