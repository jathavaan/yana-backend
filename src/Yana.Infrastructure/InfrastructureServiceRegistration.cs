using System.IdentityModel.Tokens.Jwt;

namespace Yana.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepositoryService, UserRepositoryService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEncryptionService, EncryptionService>();
        services.AddScoped<IAuthenticationService, GoogleAuthenticationService>();

        // Third party services
        services.AddScoped<JwtSecurityTokenHandler>();

        // Hosted services

        return services;
    }
}