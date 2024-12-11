using FluentValidation;
using FluentValidation.AspNetCore;
using MetaExchange.Application.Interfaces;
using MetaExchange.Application.Services;
using MetaExchange.Core.Interfaces;
using MetaExchange.Core.Services;
using MetaExchange.JsonProvider.Configurations;
using MetaExchange.JsonProvider.Interfaces;
using MetaExchange.JsonProvider.Repositories;
using MetaExchange.JsonProvider.Services;
using MetaExchange.WebApi.Contracts.Validators;

namespace MetaExchange.WebApi.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true);
        
        builder.Services.Configure<JsonProviderOptions>(builder.Configuration.GetSection("JsonProviderOptions"));
        
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<ExecutionPlanRequestValidator>();
        
        builder.Services.AddTransient<IReadOnlyFileService, FileService>();
        builder.Services.AddTransient<IReadOnlyExchangeJsonRepository, ExchangeJsonRepository>();
        builder.Services.AddTransient<IExchangeAggregatorService, ExchangeAggregatorService>();
        builder.Services.AddTransient<IBuyService, BuyService>();
        builder.Services.AddTransient<ISellService, SellService>();
        builder.Services.AddTransient<IOrderBookService, OrderBookService>();
        
        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer(); 
        builder.Services.AddSwaggerGen();
    }
}