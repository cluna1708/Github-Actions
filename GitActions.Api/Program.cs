using GitActions.Api.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

/*builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("Redis");
    return ConnectionMultiplexer.Connect(connectionString);
});*/

builder.Services.AddStackExchangeRedisCache(redisOptions =>
{
    var connectionString = builder.Configuration.GetConnectionString("Redis");

    redisOptions.Configuration = connectionString;
});
// Add services to the container.

builder.Services.AddScoped<IWeatherService, WeatherService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
