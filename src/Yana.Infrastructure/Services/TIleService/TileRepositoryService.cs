using Yana.Application.Contracts.TileService;

namespace Yana.Infrastructure.Services.TIleService;

public sealed class TileRepositoryService : ITileRepositoryService
{
    private readonly YanaDbContext _dbContext;
    private readonly IDocumentRepositoryService _documentRepositoryService;

    public TileRepositoryService(YanaDbContext dbContext, IDocumentRepositoryService documentRepositoryService)
    {
        _dbContext = dbContext;
        _documentRepositoryService = documentRepositoryService;
    }

    public async Task<Tile?> GetTile(string tileId)
        => await _dbContext.Tiles.FirstOrDefaultAsync(x => x.Id == tileId);


    public async Task<bool> SaveTile(TileDto dto)
    {
        var document = await _documentRepositoryService.GetDocument(dto.DocumentId);
        if (document is null) return false;

        var tile = await GetTile(dto.Id);
        switch (tile)
        {
            case null:
                tile = new Tile
                {
                    Id = dto.Id,
                    Content = dto.Content,
                    Document = document
                };
                _dbContext.Tiles.Add(tile);
                break;
            case not null:
                _dbContext.Tiles.Update(tile);
                break;
        }

        await _dbContext.SaveChangesAsync();
        return true;
    }
}