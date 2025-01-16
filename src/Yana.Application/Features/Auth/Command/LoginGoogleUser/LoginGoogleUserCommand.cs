namespace Yana.Application.Features.Auth.Command.LoginGoogleUser;

public sealed class LoginGoogleUserCommand(UserProfile user)
    : Command<CommandResponse<bool>>
{
    public UserProfile User { get; } = user;
}