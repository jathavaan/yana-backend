namespace Yana.Application.Features.Document.Command.DeleteTile;

public sealed class DeleteTileCommand(UserProfile user, string documentId, string tileId) : Command<CommandResponse<bool>>
{
    public UserProfile User { get; set; } = user;
    public string DocumentId { get; set; } = documentId;
    public string TileId { get; set; } = tileId;
}