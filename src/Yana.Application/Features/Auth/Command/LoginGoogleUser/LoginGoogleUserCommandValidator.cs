namespace Yana.Application.Features.Auth.Command.LoginGoogleUser;

public class LoginGoogleUserCommandValidator : AbstractValidator<LoginGoogleUserCommand>
{
    public LoginGoogleUserCommandValidator()
    {
        RuleFor(x => x.Dto)
            .NotEmpty();

        RuleFor(x => x.Dto.AuthorizationCode)
            .NotEmpty();
    }
}