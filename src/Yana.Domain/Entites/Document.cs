namespace Yana.Domain.Entites;

public class Document
{
    public string Id { get; set; } = null!;
    public DocumentType Type { get; set; }
    public GridSize GridSize { get;set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public virtual ICollection<DocumentReference> DocumentReferences { get; set; } = new List<DocumentReference>();
    public virtual ICollection<Tile> Tiles { get; set; } = new List<Tile>();
    public virtual ICollection<Citation> Citations { get; set; } = new List<Citation>();
}