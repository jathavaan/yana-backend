namespace Yana.Domain.Entites;

public class TileHasUser
{
    public string TileId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public DateTime EditedDate { get; set; }

    public virtual Tile Tile { get; set; } = null!;
    public virtual UserProfile UserProfile { get; set; } = null!;
}