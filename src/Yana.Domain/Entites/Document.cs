namespace Yana.Domain.Entites;

public class Document
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public DocumentType Type { get; set; }
    public GridSize GridSize { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public virtual ICollection<UserProfile> Users { get; set; } = new List<UserProfile>();
    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public virtual ICollection<Document> ParentDocuments { get; set; } = new List<Document>();
    public virtual ICollection<Document> ChildDocuments { get; set; } = new List<Document>();
    public virtual ICollection<Tile> Tiles { get; set; } = new List<Tile>();
    public virtual ICollection<Citation> Citations { get; set; } = new List<Citation>();
    public virtual ICollection<DocumentLayout> DocumentLayouts { get; set; } = new List<DocumentLayout>();
}