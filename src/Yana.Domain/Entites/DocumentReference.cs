namespace Yana.Domain.Entites;

public class DocumentReference
{
    public string Id { get; set; } = null!;

    public virtual Document ParentDocument { get; set; } = null!;
    public virtual Document ChildDocument { get; set; } = null!;
}
