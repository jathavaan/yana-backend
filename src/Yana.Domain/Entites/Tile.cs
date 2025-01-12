namespace Yana.Domain.Entites;

public class Tile
{
    public int Id { get; set; }
    public string? Content { get; set; }
    public int XPosition { get; set; }
    public int YPosition { get; set; }
    public int Width { get; set; }
    public int? Height { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public virtual Document Document { get; set; } = null!;
    public virtual ICollection<UserProfile> Users { get; set; } = new List<UserProfile>();
}