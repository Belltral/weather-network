using System.Text.Json;
using WeatherNetwork.HelperUtils;
using WeatherNetwork.Models.Base;
using WeatherNetwork.Services.Contracts;

namespace WeatherNetwork.Services;

public class WeatherService : IWeatherService
{
    private const string coordinatesQuery = "?latitude=-23.5475&longitude=-46.6361"; // Deve estar sempre em primeiro
    private const string currentWeatherQuery = "&current=temperature_2m,relative_humidity_2m,apparent_temperature,is_day,precipitation,wind_speed_10m,wind_direction_10m,weather_code";
    private const string hourlyWeatherQuery = "&hourly=temperature_2m,relative_humidity_2m,precipitation_probability,wind_speed_80m,wind_direction_80m,uv_index,weather_code";
    private const string dailyWeatherQuery = "&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset,daylight_duration,sunshine_duration,uv_index_max,precipitation_sum,precipitation_hours,precipitation_probability_max,wind_speed_10m_max,wind_direction_10m_dominant,weather_code";
    private const string otherOptionsQuery = "&timezone=auto&forecast_days=1";

    private const string allDayQuery = coordinatesQuery+currentWeatherQuery+hourlyWeatherQuery+dailyWeatherQuery+otherOptionsQuery;
    
    private const string clientName = "WeatherApi";
    private const string apiEndpoint = "/v1/forecast";

    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _serializerOptions;

    private Weather? weather;

    public WeatherService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _serializerOptions = new JsonSerializerOptions 
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = new PascalCaseWithNumberPolicy()
        };
    }

    public async Task<Weather?> GetFullWeather(double latitude = 0, double longitude = 0)
    {
        var client = _clientFactory.CreateClient(clientName);

        using (var response = await client.GetAsync(apiEndpoint + allDayQuery))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                weather = await JsonSerializer.DeserializeAsync<Weather>(apiResponse, _serializerOptions);

                return weather;
            }
        }

        return null;
    }
}