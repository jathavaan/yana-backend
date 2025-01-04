namespace Yana.Api.Configurations;

public static class AppConfiguration
{
    public static WebApplication ConfigureApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment()) app.ConfigureSwagger();

        app.UseHttpsRedirection();
        app.MapControllers();

        return app;
    }

    private static WebApplication ConfigureSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.EnableDeepLinking();
            options.DefaultModelsExpandDepth(0);
        });

        return app;
    }
}