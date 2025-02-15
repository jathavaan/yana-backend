namespace Yana.Domain.Entites;

public class DocumentLayout
{
    public  string TileId { get; set; } = null!;
    public LayoutSize LayoutSize { get; set; }
    public int XPosition { get; set; }
    public int YPosition { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public virtual Tile Tile { get; set; } = null!;
    public virtual Document Document { get; set; } = null!;
}