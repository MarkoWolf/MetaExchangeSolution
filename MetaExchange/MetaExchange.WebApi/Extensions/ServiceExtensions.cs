namespace MetaExchange.WebApi.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers(); 
        builder.Services.AddEndpointsApiExplorer(); 
        builder.Services.AddSwaggerGen();
    }
}