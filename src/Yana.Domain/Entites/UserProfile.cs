namespace Yana.Domain.Entites;

public class UserProfile
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime CreatedDate { get; init; } = DateTime.Now;

    public virtual ICollection<ExternalUserProfile> ExternalUserProfiles { get; set; } =
        new List<ExternalUserProfile>();

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
    public virtual ICollection<Tile> Tiles { get; set; } = new List<Tile>();
    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public virtual ICollection<Citation> Citations { get; set; } = new List<Citation>();
}