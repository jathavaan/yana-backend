namespace Yana.Api.Options;

public sealed class GoogleAuthenticationOptions
{
    public static string SectionName => "GoogleAuthentication";
    public string IssuerUri { get; init; } = null!;
    public string ClientId { get; init; } = null!;
    public string ClientSecret { get; init; } = null!;
}