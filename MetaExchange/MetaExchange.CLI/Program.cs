using MetaExchange.CLI;
using MetaExchange.Core.Interfaces;
using MetaExchange.Core.Services;
using MetaExchange.JsonProvider.Configurations;
using MetaExchange.JsonProvider.Interfaces;
using MetaExchange.JsonProvider.Repositories;
using MetaExchange.JsonProvider.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        services.Configure<JsonProviderOptions>(context.Configuration.GetSection("JsonProviderOptions"));
        services.AddTransient<IReadOnlyFileService, FileService>();
        services.AddTransient<IReadOnlyExchangeJsonRepository, ExchangeJsonRepository>();
        services.AddTransient<IExchangeAggregatorService, ExchangeAggregatorService>();
        services.AddSingleton<IBuyService, BuyService>();
        services.AddSingleton<ISellService, SellService>();
        services.AddSingleton<App>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var app = scope.ServiceProvider.GetRequiredService<App>();
app.RunAsync();