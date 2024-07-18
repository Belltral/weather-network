using System.Text.Json;
using System.Text.Json.Serialization;
using Weather_Network.Models;
using Weather_Network.Services.Contracts;

namespace Weather_Network.Services
{
    public class CurrentWeatherService : ICurrentWeatherService
    {
        private const string tempTestEndpoint = "?latitude=-23.5475&longitude=-46.6361&current=temperature_2m,relative_humidity_2m,apparent_temperature,is_day,precipitation,wind_speed_10m,wind_direction_10m&timezone=America%2FSao_Paulo&forecast_days=1";

        private const string clientName = "WeatherApi";
        private const string apiEndpoint = "/v1/forecast";

        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _options;
        private CurrentWeatherViewModel? currentWeatherVM;

        public CurrentWeatherService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip
            };
        }

        public async Task<CurrentWeatherViewModel?> GetCurrentWeather()
        {
            var client = _clientFactory.CreateClient(clientName);

            using (var response = await client.GetAsync(apiEndpoint + tempTestEndpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    currentWeatherVM = await JsonSerializer.DeserializeAsync<CurrentWeatherViewModel>(apiResponse, _options);

                    return currentWeatherVM;
                }
            }

            return null;
        }
    }
}
