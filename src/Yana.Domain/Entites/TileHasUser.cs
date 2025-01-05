namespace Yana.Domain.Entites;

public class TileHasUser
{
    public virtual Tile Tile { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}