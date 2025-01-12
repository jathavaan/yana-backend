using System.Runtime.InteropServices.JavaScript;

namespace Yana.Domain.Entites;

public class ExternalUserProfile
{
    public string Id { get; set; } = null!;
    public AuthProvider AuthProvider { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public virtual UserProfile User { get; set; } = null!;
}