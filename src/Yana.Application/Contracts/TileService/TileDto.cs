namespace Yana.Application.Contracts.TileService;

public record TileDto(string Id, string Content, string DocumentId);

public sealed record SaveTileDto(
    string Id,
    string Content,
    string DocumentId,
    TileLayout LargeLayout,
    TileLayout MediumLayout,
    TileLayout SmallLayout,
    TileLayout XSmallLayout,
    TileLayout XxSmallLayout
) : TileDto(Id, Content, DocumentId);

public sealed record TileLayout(int XPosition, int YPosition, int Width, int Height);