namespace Yana.Application.Features.Document.Command.SaveTile;

public class SaveTileCommand(string documentId, string tileId, SaveTileDto dto) : Command<CommandResponse<bool>>
{
    public string DocumentId { get; init; } = documentId;
    public string TileId { get; init; } = tileId;
    public SaveTileDto Dto { get; init; } = dto;
};

public record SaveTileDto(
    string Content,
    TileLayout LargeLayout,
    TileLayout MediumLayout,
    TileLayout SmallLayout,
    TileLayout XSmallLayout,
    TileLayout XxSmallLayout
) : TileDtoBase(Content, LargeLayout, MediumLayout, SmallLayout, XSmallLayout, XxSmallLayout);