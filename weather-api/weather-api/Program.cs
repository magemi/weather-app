using System.Net;
using weather_api.Interfaces;
using weather_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

builder.Services.AddHttpClient<WeatherDataRetriever>()
    .ConfigurePrimaryHttpMessageHandler(x => new HttpClientHandler()
    {
        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
    });

builder.Services.AddSingleton<IWeatherDataRetriever, WeatherDataRetriever>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors();

var cities = new[]
{
    "London,GB",
    "Vienna,AT",
    "Ljubljana,SI",
    "Belgrade,RS",
    "Valletta,MT"
};

app.MapGet("/getWeatherData", async () =>
{
    var weatherDataRetriever = app.Services.GetService<IWeatherDataRetriever>();
    if (weatherDataRetriever != null)
    {
        var weathers = await weatherDataRetriever!.GetWeathersAsync(cities.ToList());
        return Results.Ok(weathers);
    }

    return Results.StatusCode(500);
})
.WithName("getWeatherData")
.WithOpenApi();

app.MapPost("/setTemperatureUnit", (string temperatureUnit) =>
{
    var weatherDataRetriever = app.Services.GetService<IWeatherDataRetriever>();
    if (weatherDataRetriever != null)
    {
        weatherDataRetriever.TemperatureUnit = temperatureUnit;
        return Results.Ok();
    }

    return Results.StatusCode(500);
})
.WithName("setTemperatureUnit")
.WithOpenApi();

app.Run();
