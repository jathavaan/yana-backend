using Yana.Application.Contracts.TileService;

namespace Yana.Application.Features.Document.Command.SaveTile;

public sealed class SaveTileCommand : Command<CommandResponse<bool>>
{
    public SaveTileDto Dto { get; set; } = null!;
}