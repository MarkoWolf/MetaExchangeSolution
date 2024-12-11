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

    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                ILogger<Program> logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                IExceptionHandlerPathFeature? exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                if (exceptionHandlerPathFeature?.Error is not null)
                {
                    logger.LogError(
                        exceptionHandlerPathFeature.Error,
                        "Error occurred at {Path}: {Message}",
                        context.Request.Path,
                        exceptionHandlerPathFeature.Error.Message);

                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var errorResponse = new
                    {
                        Message = "An internal server error occurred.",
                        Detail = exceptionHandlerPathFeature.Error.Message
                    };

                    await context.Response.WriteAsJsonAsync(errorResponse);
                }
            });
        });
    }
}