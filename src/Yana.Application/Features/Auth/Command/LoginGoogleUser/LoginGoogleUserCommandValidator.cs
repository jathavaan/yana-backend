namespace Yana.Application.Features.Auth.Command.LoginGoogleUser;

public class LoginGoogleUserCommandValidator : AbstractValidator<LoginGoogleUserCommand>
{
    public LoginGoogleUserCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("Refresh token must be provided");
    }
}