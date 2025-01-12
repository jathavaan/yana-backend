namespace Yana.Application.Features.Auth.Command.LoginGoogleUser;

public class LoginGoogleUserCommand(UserProfile user, string refreshToken) : Command<CommandResponse<bool>>
{
    public UserProfile User { get; } = user;
    public string RefreshToken { get; } = refreshToken;
}