using System.Text.Json;
using Weather_Network.HelperUtils;
using Weather_Network.Models;
using Weather_Network.Services.Contracts;

namespace Weather_Network.Services;

public class WeatherService : IWeatherService
{
    private const string allDay = "?latitude=-23.5475&longitude=-46.6361&current=temperature_2m,relative_humidity_2m,apparent_temperature" +
        ",is_day,precipitation,wind_speed_10m,wind_direction_10m&hourly=temperature_2m,relative_humidity_2m,precipitation_probability" +
        ",wind_speed_80m,wind_direction_80m&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset,daylight_duration,sunshine_duration" +
        ",uv_index_max,precipitation_sum,precipitation_hours,precipitation_probability_max,wind_speed_10m_max" +
        ",wind_direction_10m_dominant&timezone=auto&forecast_days=1";
    private const string currentWeather = "?latitude=-23.5475&longitude=-46.6361&current=temperature_2m,relative_humidity_2m,apparent_temperature" +
        ",is_day,precipitation,wind_speed_10m,wind_direction_10m&timezone=auto&forecast_days=1";

    private const string clientName = "WeatherApi";
    private const string apiEndpoint = "/v1/forecast";

    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _serializerOptions;

    private WeatherViewModel? weatherVM;
    private CurrentWeatherViewModel? currentWeatherVM;
    private DailyWeatherViewModel? dailyWeatherVM;
    private HourlyWeatherViewModel? hourlyWeatherVM;

    public WeatherService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _serializerOptions = new JsonSerializerOptions 
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = new PascalCaseWithNumberPolicy()
        };
    }

    public async Task<WeatherViewModel?> GetWeather(double latitude = 0, double longitude = 0)
    {
        var client = _clientFactory.CreateClient(clientName);

        using (var response = await client.GetAsync(apiEndpoint + currentWeather))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                weatherVM = await JsonSerializer.DeserializeAsync<WeatherViewModel>(apiResponse, _serializerOptions);

                return weatherVM;
            }
        }

        return null;
    }
}