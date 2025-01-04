namespace Yana.Domain.Entites;

public class User
{
    public string Id { get; set; } = null!;
    public AuthProvider AuthProvider { get; set; }
    public DateTime LastLogindDate { get; set; }
}