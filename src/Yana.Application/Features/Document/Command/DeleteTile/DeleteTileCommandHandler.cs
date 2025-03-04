namespace Yana.Application.Features.Document.Command.DeleteTile;

public sealed class DeleteTileCommandHandler : IRequestHandler<DeleteTileCommand, CommandResponse<bool>>
{
    private readonly IDocumentRepositoryService _documentRepositoryService;
    private readonly ITileRepositoryService _tileRepositoryService;

    public DeleteTileCommandHandler(ITileRepositoryService tileRepositoryService,
        IDocumentRepositoryService documentRepositoryService)
    {
        _tileRepositoryService = tileRepositoryService;
        _documentRepositoryService = documentRepositoryService;
    }


    public async Task<CommandResponse<bool>> Handle(DeleteTileCommand request, CancellationToken cancellationToken)
    {
        var hasPermission = await _documentRepositoryService.HasUserDocumentPermission(
            request.User,
            request.DocumentId,
            DocumentRole.Editor
        );

        if (!hasPermission)
        {
            return new CommandResponse<bool>()
            {
                ErrorCode = ErrorCode.Forbidden,
                ErrorMessage =
                    $"{request.User.Id} does not have permission to delete tile in the following document: {request.DocumentId}"
            };
        }

        var result = await _tileRepositoryService.DeleteTile(request.DocumentId, request.TileId);
        return result
            ? new CommandResponse<bool>()
            {
                Result = result
            }
            : new CommandResponse<bool>()
            {
                ErrorCode = ErrorCode.NotFound,
                ErrorMessage = $"Could not find tile {request.TileId} in document {request.DocumentId}"
            };
    }
}