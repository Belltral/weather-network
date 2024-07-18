using Weather_Network.Models;

namespace Weather_Network.Services.Contracts;

public interface IWeatherService
{
    public Task<WeatherViewModel?> GetWeather(double latitude, double longitude);
}