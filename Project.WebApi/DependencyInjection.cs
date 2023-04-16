using Microsoft.AspNetCore.Mvc.Infrastructure;
using Project.WebApi.Common.Mapping;
using Project.WebApi.Errors;

namespace Project.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        //builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>()); // replaced with error endpoint - custom problem detais factory
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, ApplicationProblemDetailsFactory>();
        services.AddMappings();
        return services;
    }
}