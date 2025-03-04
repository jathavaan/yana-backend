namespace Yana.Domain.Entites;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime DateCreated { get; init; } = DateTime.Now;

    public virtual UserProfile UserProfile { get; set; } = null!;
    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
}