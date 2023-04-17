using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Authentication.Commands.Register;
using Project.Application.Authentication.Common;
using Project.Application.Common.Behaviors;
using System.Reflection;

namespace Project.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(config =>
                config.RegisterServicesFromAssemblies(assembly));

        services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationPipelineBehavior<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(),
            includeInternalTypes: true); // instead register every single validator alone register all with reflection

        //services.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>();





        return services;
    }
}
