namespace Yana.Api.Options;

public class ConnectionStringsOptions
{
    public static string SectionName = "ConnectionStrings";
    public string YanaDb { get; init; } = null!;
}