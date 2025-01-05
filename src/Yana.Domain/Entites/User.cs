namespace Yana.Domain.Entites;

public class User
{
    public string Id { get; set; } = null!;
    public AuthProvider AuthProvider { get; set; }
    public DateTime LastLogindDate { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
    public virtual ICollection<Tile> Tiles { get; set; } = new List<Tile>();
    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public virtual ICollection<Citation> Citations { get; set; } = new List<Citation>();
}