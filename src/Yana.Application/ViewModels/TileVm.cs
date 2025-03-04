namespace Yana.Application.ViewModels;

public record TileVm(string TileId, string? Content, ICollection<LayoutVm> TileLayouts);

public record LayoutVm(LayoutSize LayoutSize, int XPosition, int YPosition, int Width, int Height);