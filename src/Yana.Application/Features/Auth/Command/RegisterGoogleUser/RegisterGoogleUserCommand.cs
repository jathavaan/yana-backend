namespace Yana.Application.Features.Auth.Command.RegisterGoogleUser;

public sealed class RegisterGoogleUserCommand(UserProfile user, string refreshToken) : Command<CommandResponse<UserVm>>
{
    public UserProfile User { get; } = user;
    public string RefreshToken { get; set; } = refreshToken;
}