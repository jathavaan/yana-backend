using Yana.Application.Middleware;

namespace Yana.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToArray();

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(assemblies);

            configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ErrorHandlingPipelineBehaviour<,>));
            configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CommandValidatorPipelineBehaviour<,>));
            configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehaviour<,>));
        });

        return services;
    }
}