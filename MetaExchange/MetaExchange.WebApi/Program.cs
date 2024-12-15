using MetaExchange.WebApi.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build()) // Lies Konfiguration aus appsettings.json
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up the application...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.ConfigureServices();

    builder.Host.UseSerilog();

    var app = builder.Build();

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
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}

