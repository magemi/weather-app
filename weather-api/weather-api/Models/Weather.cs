using System.Text.Json.Serialization;

namespace weather_api.Models;

public record Weather(string Description, string Icon);
public record Sys(long Sunrise, long Sunset);
public record Main(decimal Temp);
public record Root(IReadOnlyList<Weather> Weather, Main Main, Sys Sys, string Name);

public record WeatherViewModel
{
    public string? City { get; set; }

    public string? Description { get; set; }

    public string? Icon { get; set; }

    public decimal Temperature { get; set; }

    public long Sunrise { get; set; }

    public long Sunset { get; set; }
};