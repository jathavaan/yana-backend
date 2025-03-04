namespace Yana.Application.Features.Document.Command.DeleteTile;

public sealed class DeleteTileCommandValidator : AbstractValidator<DeleteTileCommand>
{
    public DeleteTileCommandValidator()
    {
        RuleFor(x => x.User)
            .NotNull()
            .WithMessage("User must be provided. This could be due the the user not being logged in");

        RuleFor(x => x.DocumentId)
            .NotEmpty()
            .WithMessage("Document ID cannot be null or empty");

        RuleFor(x => x.TileId)
            .NotEmpty()
            .WithMessage("Tile ID cannot be null or empty");
    }
}