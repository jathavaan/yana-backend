namespace Yana.Domain.Entites;

public class Tile
{
    public string Id { get; set; } = null!;
    public string? Content { get; set; }
    public DateTime CreatedDate { get; init; } = DateTime.Now;

    public virtual Document Document { get; set; } = null!;
    public virtual ICollection<UserProfile> Users { get; set; } = new List<UserProfile>();
    public virtual ICollection<DocumentLayout> DocumentLayouts { get; set; } = new List<DocumentLayout>();
}