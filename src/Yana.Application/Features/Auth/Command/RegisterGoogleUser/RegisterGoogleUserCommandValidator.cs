namespace Yana.Application.Features.Auth.Command.RegisterGoogleUser;

public class RegisterGoogleUserCommandValidator : AbstractValidator<RegisterGoogleUserCommand>
{
    public RegisterGoogleUserCommandValidator()
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