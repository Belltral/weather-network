using System.Globalization;
using System.Text.Json;
using WeatherNetwork.HelperUtils;
using WeatherNetwork.Models.Base;
using WeatherNetwork.Services.Contracts;

namespace WeatherNetwork.Services;

public class HourlyWeatherService : IHourlyWeatherService
{
    private const string hourlyWeatherQuery = "&hourly=temperature_2m,relative_humidity_2m,precipitation_probability,wind_speed_80m,wind_direction_80m,uv_index,weather_code";
    private const string otherOptionsQuery = "&timezone=auto&forecast_hours=24";

    private const string hourlyQuery = hourlyWeatherQuery + otherOptionsQuery;

    private const string clientName = "WeatherApi";
    private const string apiEndpoint = "/v1/forecast";

    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _serializerOptions;

    private HourlyWeather? hourlyWeather;

    public HourlyWeatherService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = new PascalCaseWithNumberPolicy()
        };
    }

    public async Task<HourlyWeather?> GetHourly(double latitude, double longitude)
    {
        var client = _clientFactory.CreateClient(clientName);

        using (var response = await client.GetAsync(apiEndpoint 
            + $"?latitude={latitude.ToString(CultureInfo.InvariantCulture)}&longitude={longitude.ToString(CultureInfo.InvariantCulture)}"
            + hourlyQuery))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                Weather weather = await JsonSerializer.DeserializeAsync<Weather>(apiResponse, _serializerOptions);

                hourlyWeather = weather!.Hourly;

                return hourlyWeather;
            }

            return null;
        }
    }
}
