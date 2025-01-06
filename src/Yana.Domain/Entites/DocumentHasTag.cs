namespace Yana.Domain.Entites;

public class DocumentHasTag
{
    public string DocumentId { get; set; } = null!;
    public int TagId { get; set; }

    public virtual Document Document { get; set; } = null!;
    public virtual Tag Tag { get; set; } = null!;
}