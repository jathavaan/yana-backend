namespace Yana.Application.Features.Document.Command.SaveTile;

public class SaveTileCommandHandler : IRequestHandler<SaveTileCommand, CommandResponse<bool>>
{
    private readonly ITileRepositoryService _tileRepositoryService;

    public SaveTileCommandHandler(IDocumentRepositoryService documentRepositoryService,
        ITileRepositoryService tileRepositoryService)
    {
        _tileRepositoryService = tileRepositoryService;
    }

    public async Task<CommandResponse<bool>> Handle(SaveTileCommand request, CancellationToken cancellationToken)
    {
        var result = await _tileRepositoryService.SaveTile(request.Dto);

        return new CommandResponse<bool>()
        {
            Result = result
        };
    }
}