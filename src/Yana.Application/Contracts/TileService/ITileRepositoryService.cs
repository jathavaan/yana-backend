namespace Yana.Application.Contracts.TileService;

public interface ITileRepositoryService
{
    public Task<Tile?> GetTile(string tileId);
    public Task<bool> SaveTile(TileDto dto);
    public Task<bool> DeleteTile(string doucmentId, string tileId);
}