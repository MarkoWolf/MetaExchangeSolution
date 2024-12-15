using MetaExchange.CLI;
using MetaExchange.CLI.Interfaces;
using MetaExchange.CLI.Services;
using MetaExchange.Core.Interfaces;
using MetaExchange.Core.Services;
using MetaExchange.JsonProvider.Configurations;
using MetaExchange.JsonProvider.Interfaces;
using MetaExchange.JsonProvider.Repositories;
using MetaExchange.JsonProvider.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

try
{
    var host = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, config) => { config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); })
        .ConfigureServices((context, services) =>
        {
            services.Configure<JsonProviderOptions>(context.Configuration.GetSection("JsonProviderOptions"));
            services.AddTransient<IReadOnlyFileService, FileService>();
            services.AddTransient<IReadOnlyExchangeJsonRepository, ExchangeJsonRepository>();
            services.AddTransient<IExchangeAggregatorService, ExchangeAggregatorService>();
            services.AddTransient<IBuyService, BuyService>();
            services.AddTransient<ISellService, SellService>();
            services.AddTransient<IUserInteractionService, UserInteractionService>();
            services.AddTransient<IOrderHandler, OrderHandler>();
            services.AddSingleton<App>();
        }) .ConfigureLogging((context, logging) =>
        {
            logging.ClearProviders();             
            logging.AddConsole();                 
            logging.AddDebug();                   
            logging.AddConfiguration(context.Configuration.GetSection("Logging")); 
        })
        .Build();
    
    var logger = host.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Starting MetaExchange CLI application...");
    
    using var scope = host.Services.CreateScope();
    var app = scope.ServiceProvider.GetRequiredService<App>(); 
    app.Run();
}
catch (Exception ex)
{
    using var loggerFactory = LoggerFactory.Create(logging =>
    {
        logging.AddConsole(); 
    });
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogCritical(ex, "An unexpected error occurred while starting the application.");
}