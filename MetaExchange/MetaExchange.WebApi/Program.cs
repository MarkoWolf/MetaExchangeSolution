using MetaExchange.WebApi.Extensions;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    builder.ConfigureServices();

    var app = builder.Build();

    var logger = app.Services.GetRequiredService<ILogger<Program>>();

    logger.LogInformation("Starting up the application...");

    if (app.Environment.IsDevelopment())
    {
        app.UseSwaggerWithUI();
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwaggerWithUI();
    }

    app.ConfigureCustomMiddleware();

    app.MapControllers();

    await app.RunAsync();
}
catch (Exception ex)
{
    using var loggerFactory = LoggerFactory.Create(loggingBuilder => { loggingBuilder.AddConsole(); });
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogCritical(ex, "Application start-up failed");
}