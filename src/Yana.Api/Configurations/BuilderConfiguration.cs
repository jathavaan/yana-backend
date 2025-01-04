namespace Yana.Api.Configurations;

internal static class BuilderConfiguration
{
    internal static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices();

        builder.ConfigureControllers();
        builder.ConfigureSwagger();

        return builder;
    }


    private static WebApplicationBuilder ConfigureControllers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        return builder;
    }

    private static void ConfigureSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options => { options.ExampleFilters(); });
        builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
    }
}