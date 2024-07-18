using Weather_Network.Models;

namespace Weather_Network.Services.Contracts
{
    public interface ICurrentWeatherService
    {
        public Task<CurrentWeatherViewModel?> GetCurrentWeather();
    }
}
