using Microsoft.AspNetCore.Diagnostics;

namespace MetaExchange.WebApi.Extensions;

public static class MiddlewareExtensions
{
    public static void ConfigureCustomMiddleware(this WebApplication app)
    {
        app.UseRouting();
        app.UseAuthorization();
    }

    public static void UseSwaggerWithUI(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetaExchange API v1");
            c.RoutePrefix = string.Empty;
        });
    }
}