namespace Yana.Api.Options;

public sealed class GoogleAuthenticationOptions
{
    public static string SectionName => "GoogleAuthentication";
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
}