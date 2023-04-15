﻿
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
namespace Project.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(config =>
                config.RegisterServicesFromAssemblies(assembly));
        services.AddValidatorsFromAssembly(assembly); // for fluent validations on models
        return services;
    }
}
