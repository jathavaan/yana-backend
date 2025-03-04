namespace Yana.Application.Features.Document.Command.CreateDocument;

public sealed class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, CommandResponse<DocumentVm>>
{
    private readonly IDocumentRepositoryService _documentRepositoryService;

    public CreateDocumentCommandHandler(IDocumentRepositoryService documentRepositoryService)
    {
        _documentRepositoryService = documentRepositoryService;
    }

    public async Task<CommandResponse<DocumentVm>> Handle(CreateDocumentCommand request,
        CancellationToken cancellationToken)
    {
        var document = await _documentRepositoryService.CreateDocument(request.User, request.Dto);
        return new CommandResponse<DocumentVm>()
        {
            Result = new DocumentVm(
                Id: document.Id,
                Title: document.Title,
                Tags: document.Tags.Select(x => new TagVm(x.Id, x.Name)).ToList(),
                Tiles: document.Tiles.Select(x => new TileVm(
                    TileId: x.Id,
                    Content: x.Content,
                    TileLayouts: x.DocumentLayouts
                        .Select(y => new LayoutVm(
                            LayoutSize: y.LayoutSize,
                            XPosition: y.XPosition,
                            YPosition: y.YPosition,
                            Width: y.Width,
                            Height: y.Height
                        ))
                        .ToList()
                )).ToList()
            )
        };
    }
}