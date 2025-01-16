namespace Yana.Application.Features.Auth.Command.LoginGoogleUser;

public class LoginGoogleUserCommandValidator : AbstractValidator<LoginGoogleUserCommand>
{
    public LoginGoogleUserCommandValidator()
    {
        RuleFor(x => x.User)
            .NotNull();

        RuleFor(x => x.User.Id)
            .NotEmpty();

        RuleFor(x => x.User.Email)
            .EmailAddress()
            .NotEmpty();
    }
}