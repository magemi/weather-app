using weather_api.Models;

namespace weather_api.Interfaces;

public interface IWeatherDataRetriever
{
    string TemperatureUnit { get; set; }

    public Task<IEnumerable<WeatherViewModel>?> GetWeathersAsync(List<String> cities);
}
