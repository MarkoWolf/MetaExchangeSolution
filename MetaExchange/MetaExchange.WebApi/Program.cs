using MetaExchange.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUI();
}

app.ConfigureCustomMiddleware();

app.MapControllers(); 

app.Run();


