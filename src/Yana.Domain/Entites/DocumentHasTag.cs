namespace Yana.Domain.Entites;

public class DocumentHasTag
{
    public virtual Document Document { get; set; } = null!;
    public virtual Tag Tag { get; set; } = null!;
}