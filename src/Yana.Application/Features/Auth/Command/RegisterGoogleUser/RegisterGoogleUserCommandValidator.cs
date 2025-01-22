namespace Yana.Application.Features.Auth.Command.RegisterGoogleUser;

public class RegisterGoogleUserCommandValidator : AbstractValidator<RegisterGoogleUserCommand>
{
    public RegisterGoogleUserCommandValidator()
    {
        RuleFor(x => x.Dto)
            .NotNull();

        RuleFor(x => x.Dto.AuthorizationCode)
            .NotEmpty()
            .WithMessage("Authorization code is required");
    }
}