using Yana.Application.Contracts.TileService;

namespace Yana.Infrastructure.Services.TileService;

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
        => await _dbContext.Tiles
            .Include(x => x.DocumentLayouts)
            .FirstOrDefaultAsync(x => x.Id == tileId);


    public async Task<bool> SaveTile(TileDto dto)
    {
        var document = await _documentRepositoryService.GetDocument(dto.DocumentId);
        if (document is null) return false;

        var tile = await GetTile(dto.Id);
        var largeLayout = ConvertTileLayoutToDocumentLayout(dto.LargeLayout, LayoutSize.Large);
        var mediumLayout = ConvertTileLayoutToDocumentLayout(dto.MediumLayout, LayoutSize.Medium);
        var smallLayout = ConvertTileLayoutToDocumentLayout(dto.LargeLayout, LayoutSize.Small);
        var xSmallLayout = ConvertTileLayoutToDocumentLayout(dto.LargeLayout, LayoutSize.XSmall);
        var xxSmallLayout = ConvertTileLayoutToDocumentLayout(dto.LargeLayout, LayoutSize.XxSmall);

        switch (tile)
        {
            case null:
                tile = new Tile
                {
                    Id = dto.Id,
                    Content = dto.Content,
                    Document = document,
                    DocumentLayouts = [largeLayout, mediumLayout, smallLayout, xSmallLayout, xxSmallLayout]
                };
                _dbContext.Tiles.Add(tile);
                break;
            case not null:
                tile.Content = dto.Content;
                tile.DocumentLayouts = [largeLayout, mediumLayout, smallLayout, xSmallLayout, xxSmallLayout];

                _dbContext.Tiles.Update(tile);
                break;
        }

        await _dbContext.SaveChangesAsync();
        return true;
    }

    private DocumentLayout ConvertTileLayoutToDocumentLayout(TileLayout tileLayout, LayoutSize layoutSize)
        => new()
        {
            LayoutSize = layoutSize,
            XPosition = tileLayout.XPosition,
            YPosition = tileLayout.YPosition,
            Height = tileLayout.Height,
            Width = tileLayout.Width
        };
}