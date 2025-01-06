namespace Yana.Domain.Entites;

public class Citation
{
    public int Id { get; set; }
    public string? Author { get; set; }
    public int PublicationYear { get; set; }
    public string Title { get; set; } = null!;
    public CitationSourceType SourceType { get; set; }
    public int? Volume { get; set; }
    public int? Issue { get; set; }
    public string? Pages { get; set; }
    public string? Url { get; set; }
    public DateOnly RetrievedDate { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual Document? Document { get; set; }
}