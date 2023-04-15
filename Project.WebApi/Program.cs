using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Project.Application;
using Project.Infrastructure;
using Project.WebApi.Errors;
using Project.WebApi.Filters;
using Project.WebApi.Middleware;


// Add services to the container.
var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
           .AddApplication()
           .AddInfrastructure(builder.Configuration);

    //builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>()); // replaced with error endpoint - custom problem detais factory
    builder.Services.AddControllers();
    builder.Services.AddSingleton<ProblemDetailsFactory, ApplicationProblemDetailsFactory>();

}

var app = builder.Build();
{
    // app.UseMiddleware<ErrorHandlingMiddleware>(); // replaced with error handling attribute

    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();
}