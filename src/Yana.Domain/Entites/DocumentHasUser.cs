namespace Yana.Domain.Entites;

public class DocumentHasUser
{
    public string DocumentId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public DocumentRole Role { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public virtual Document Document { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}