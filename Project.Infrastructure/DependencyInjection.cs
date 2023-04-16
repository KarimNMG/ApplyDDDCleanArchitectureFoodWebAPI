using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Project.Application.Common.Interfaces.Authentication;
using Project.Application.Common.Interfaces.Presistance;
using Project.Application.Interfaces.Services;
using Project.Infrastructure.Authentication;
using Project.Infrastructure.Presistance;
using Project.Infrastructure.Services;
using System.Text;

namespace Project.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
         ConfigurationManager configuration)
    {
        services
            .AddAuth(configuration)
            .AddPersistance();
        
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    public static IServiceCollection AddPersistance(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMenuRepository, MenuRepository>();

        return services;
    }

    public static IServiceCollection AddAuth(
       this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var _jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, _jwtSettings);

        services.AddSingleton(Options.Create(_jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddAuthentication(defaultScheme:
            JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.Secret))
            }); // return authentication builder which in turn map authschema and coresponding auth handler
        return services;
    }
}