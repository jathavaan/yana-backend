using Yana.Application.Features.Auth.Command.LoginGoogleUser;

namespace Yana.Application.Features.Auth.Command.RegisterGoogleUser;

public sealed class RegisterGoogleUserCommand(AuthorizationCodeDto dto) : Command<CommandResponse<AuthenticationVm>>
{
    public AuthorizationCodeDto Dto { get; } = dto;
}