using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Yana.Api.Configurations;

internal static class BuilderConfiguration
{
    internal static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
    {
        builder.ConfigureOptions();

        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices();

        builder.ConfigureDatabase();

        builder.ConfigureControllers();
        builder.ConfigureSwagger();
        builder.ConfigureAuthenticationAndAuthorization();

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

        builder.Services.Configure<ConnectionStringsOptions>(
            builder.Configuration.GetSection(ConnectionStringsOptions.SectionName));
    }

    private static void ConfigureSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Yet Another Note App API", Version = "v1" });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter JWT Bearer token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });

            options.ExampleFilters();
        });
        builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
    }

    private static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDbContext<YanaDbContext>(options =>
                options.UseSqlServer(
                        builder.Configuration
                            .GetSection(ConnectionStringsOptions.SectionName)
                            .Get<ConnectionStringsOptions>()!
                            .YanaDb
                    )
                    .EnableSensitiveDataLogging());
        }
    }

    private static void ConfigureAuthenticationAndAuthorization(this WebApplicationBuilder builder)
    {
        var googleAuthenticationOptions = builder.Configuration.GetSection(GoogleAuthenticationOptions.SectionName)
            .Get<GoogleAuthenticationOptions>();

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                if (googleAuthenticationOptions is null) return;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = googleAuthenticationOptions.IssuerUri,
                    ValidAudience = googleAuthenticationOptions.ClientId,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(googleAuthenticationOptions.ClientSecret)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };

                options.Authority = googleAuthenticationOptions.IssuerUri;
                options.RequireHttpsMetadata = true;
            });

        builder.Services.AddAuthorization();
    }
}