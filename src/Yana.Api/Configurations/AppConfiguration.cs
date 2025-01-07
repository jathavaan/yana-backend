namespace Yana.Api.Configurations;

public static class AppConfiguration
{
    public static WebApplication Configure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment()) app.ConfigureSwagger();

        app.UseHttpsRedirection();
        app.ConfigureAuthenticationAndAuthorization();
        app.MapControllers();

        return app;
    }

    private static void ConfigureSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.EnableDeepLinking();
            options.DefaultModelsExpandDepth(0);
        });
    }

    private static void ConfigureAuthenticationAndAuthorization(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}