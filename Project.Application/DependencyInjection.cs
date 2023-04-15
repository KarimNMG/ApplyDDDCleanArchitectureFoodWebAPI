
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Services.Authentication;

namespace Project.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
}
