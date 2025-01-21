namespace Yana.Application.Features.Auth.Command.LoginGoogleUser;

public sealed class LoginGoogleUserCommand(AuthorizationCodeDto dto)
    : Command<CommandResponse<bool>>
{
    public AuthorizationCodeDto Dto { get; } = dto;
}

public sealed record AuthorizationCodeDto(string AuthorizationCode);
