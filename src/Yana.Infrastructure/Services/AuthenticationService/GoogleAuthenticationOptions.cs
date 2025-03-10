﻿namespace Yana.Infrastructure.Services.AuthenticationService;

public sealed class GoogleAuthenticationOptions
{
    public static string SectionName => "GoogleAuthentication";
    public string Issuer { get; init; } = null!;
    public string ClientId { get; init; } = null!;
    public string ClientSecret { get; init; } = null!;
    public string[] Scopes { get; set; } = null!;
    public string RedirectUri { get; set; } = null!;
}