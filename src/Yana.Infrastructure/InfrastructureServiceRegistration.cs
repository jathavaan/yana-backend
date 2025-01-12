namespace Yana.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IUserRepositoryService, UserRepositoryService>();
        services.AddTransient<IUserAuthService, UserAuthService>();
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<IEncryptionService, EncryptionService>();
        services.AddTransient<IAuthenticationService, GoogleAuthenticationService>();

        return services;
    }
}