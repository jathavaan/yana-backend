namespace Yana.Domain.Entites;

public class Tile
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public int XPosition { get; set; }
    public int YPosition { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public virtual Document Document { get; set; } = null!;
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}