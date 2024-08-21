using System.Globalization;
using System.Text.Json;
using WeatherNetwork.HelperUtils;
using WeatherNetwork.Models.Base;
using WeatherNetwork.Services.Contracts;

namespace WeatherNetwork.Services;

public class DailyWeatherService : IDailyWeatherService
{
    private const string dailyWeatherQuery = "&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset,daylight_duration,sunshine_duration,uv_index_max,precipitation_sum,precipitation_hours,precipitation_probability_max,wind_speed_10m_max,wind_direction_10m_dominant,weather_code";
    private const string otherOptionsQuery = "&timezone=auto&forecast_days=15";
    private const string fullDailyQuery = dailyWeatherQuery + otherOptionsQuery;

    private const string clientName = "WeatherApi";
    private const string apiEndpoint = "/v1/forecast";

    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _serializerOptions;

    private DailyWeather? dailyWeather;

    public DailyWeatherService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = new PascalCaseWithNumberPolicy()
        };
    }

    public async Task<DailyWeather?> GetDailyWeather(double latitude, double longitude)
    {
        var client = _clientFactory.CreateClient(clientName);

        using (var response = await client.GetAsync(apiEndpoint 
            + $"?latitude={latitude.ToString(CultureInfo.InvariantCulture)}&longitude={longitude.ToString(CultureInfo.InvariantCulture)}"
            + fullDailyQuery))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                var weatherInfo = await JsonSerializer.DeserializeAsync<Weather>(apiResponse, _serializerOptions);

                dailyWeather = weatherInfo!.Daily;

                return dailyWeather;
            }

            return null;
        }
    }
}
