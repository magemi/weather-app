using System.Dynamic;
using System.Text.Json;
using weather_api.Interfaces;
using weather_api.Models;

namespace weather_api.Services;

public class WeatherDataRetriever : IWeatherDataRetriever
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public string TemperatureUnit { get; set; } = "metric";

    public WeatherDataRetriever(IConfiguration config, HttpClient httpClient)
    {
        _config = config;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_config.GetValue<string>("WeatherApiInfo:ApiUrl")!);
    }

    public async Task<IEnumerable<WeatherViewModel>?> GetWeathersAsync(List<String> cities)
    {
        List<WeatherViewModel> weatherViewModels = [];
        foreach (var city in cities)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(
                $"?q={city}&limit=1&units={TemperatureUnit}&appid={_config.GetValue<string>("WeatherApiInfo:ApiKey")}");

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                var serializerOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                };

                Root? root = JsonSerializer.Deserialize<Root>(responseBody, serializerOptions);

                if (root != null)
                {
                    weatherViewModels.Add(MapWeatherDataToViewModel(root));
                }
            }
        }

        return weatherViewModels;
    }

    private WeatherViewModel MapWeatherDataToViewModel(Root root)
    {
        WeatherViewModel weatherViewModel = new WeatherViewModel();

        weatherViewModel.City = root.Name;
        weatherViewModel.Description = root.Weather[0].Description;
        weatherViewModel.Icon = $"https://openweathermap.org/img/wn/{root.Weather[0].Icon}.png";
        weatherViewModel.Temperature = root.Main.Temp;
        weatherViewModel.Sunrise = root.Sys.Sunrise;
        weatherViewModel.Sunset = root.Sys.Sunset;

        return weatherViewModel;
    }
}