using System.Globalization;
using System.Text.Json;
using WeatherNetwork.HelperUtils;
using WeatherNetwork.Models.Base;
using WeatherNetwork.Services.Contracts;

namespace WeatherNetwork.Services;

public class AirQualityIndexService : IAirQualityIndexService
{
    private const string _airQualityIndexQuery = "&current=us_aqi,pm10,pm2_5,carbon_monoxide,nitrogen_dioxide,sulphur_dioxide,ozone,uv_index";
    private const string _otherOptionsQuery = "&timezone=auto&forecast_days=1";

    private const string clientName = "AQIApi";
    private const string apiEndpoint = "v1/air-quality";

    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _serializerOptions;

    private AirQualityIndex? _airQualityIndex;

    public AirQualityIndexService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = new PascalCaseWithNumberPolicy()
        };
    }

    public async Task<AirQualityIndex?> GetAirQualityIndex(double latitude, double longitude)
    {
        var client = _clientFactory.CreateClient(clientName);

        using var response = await client.GetAsync(apiEndpoint
            + $"?latitude={latitude.ToString(CultureInfo.InvariantCulture)}&longitude={longitude.ToString(CultureInfo.InvariantCulture)}"
            + _airQualityIndexQuery
            + _otherOptionsQuery
            );

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();

            _airQualityIndex = await JsonSerializer.DeserializeAsync<AirQualityIndex>(apiResponse, _serializerOptions);

            return _airQualityIndex;
        }

        return null;
    }
}
