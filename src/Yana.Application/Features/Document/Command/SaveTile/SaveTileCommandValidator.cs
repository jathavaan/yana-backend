namespace Yana.Application.Features.Document.Command.SaveTile;

public class SaveTileCommandValidator : AbstractValidator<SaveTileCommand>
{
    public SaveTileCommandValidator()
    {
        RuleFor(x => x.Dto)
            .ChildRules(y =>
            {
                y.RuleFor(z => z.Id)
                    .NotEmpty()
                    .WithMessage("ID cannot be null or empty");

                y.RuleFor(z => z.Content)
                    .NotNull()
                    .WithMessage("Content cannont have a null value");

                y.RuleFor(z => z.DocumentId)
                    .NotEmpty()
                    .WithMessage("Document ID cannot be null or empty");

                y.RuleFor(z => z.LargeLayout)
                    .NotNull()
                    .WithMessage($"Large layout cannot be null");

                y.RuleFor(z => z.MediumLayout)
                    .NotNull()
                    .WithMessage($"Medium layout cannot be null");

                y.RuleFor(z => z.SmallLayout)
                    .NotNull()
                    .WithMessage($"Small layout cannot be null");

                y.RuleFor(z => z.XSmallLayout)
                    .NotNull()
                    .WithMessage($"XSmall layout cannot be null");

                y.RuleFor(z => z.XxSmallLayout)
                    .NotNull()
                    .WithMessage($"XXSmall layout cannot be null");
            })
            .NotEmpty();
    }
}