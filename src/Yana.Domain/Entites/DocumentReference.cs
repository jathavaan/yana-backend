namespace Yana.Domain.Entites;

public class DocumentReference
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ParentDocumentId { get; set; } = null!;
    public string ChildDocumentId { get; set; } = null!;

    public virtual Document ParentDocument { get; set; } = null!;
    public virtual Document ChildDocument { get; set; } = null!;
}