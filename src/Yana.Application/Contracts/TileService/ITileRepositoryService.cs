﻿namespace Yana.Application.Contracts.TileService;

public interface ITileRepositoryService
{
    public Task<Tile?> GetTile(string tileId);
    public Task<bool> SaveTile(SaveTileDto dto);
}