namespace Yana.Application.Contracts.TileService;

public abstract record TileDtoBase(
    string Content,
    TileLayout LargeLayout,
    TileLayout MediumLayout,
    TileLayout SmallLayout,
    TileLayout XSmallLayout,
    TileLayout XxSmallLayout
);

public record TileDto(
    string Id,
    string DocumentId,
    string Content,
    TileLayout LargeLayout,
    TileLayout MediumLayout,
    TileLayout SmallLayout,
    TileLayout XSmallLayout,
    TileLayout XxSmallLayout
) : TileDtoBase(Content, LargeLayout, MediumLayout, SmallLayout, XSmallLayout, XxSmallLayout);

public sealed record TileLayout(int XPosition, int YPosition, int Width, int Height);