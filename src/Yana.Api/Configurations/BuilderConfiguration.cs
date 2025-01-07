namespace Yana.Api.Configurations;

internal static class BuilderConfiguration
{
    internal static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.ConfigureOptions();

        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices();

        builder.ConfigureControllers();
        builder.ConfigureSwagger();
        builder.ConfigureDatabase();

        return builder;
    }


    private static void ConfigureControllers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
    }

    private static void ConfigureOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<GoogleAuthenticationOptions>(
            builder.Configuration.GetSection(GoogleAuthenticationOptions.SectionName));
    }

    private static void ConfigureSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options => { options.ExampleFilters(); });
        builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
    }

    private static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDbContext<YanaDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:YanaDb").Value)
                    .EnableSensitiveDataLogging());
        }
    }
}