using WeatherNetwork.Models.Base;

namespace WeatherNetwork.Services.Contracts;

public interface IWeatherService
{
    public Task<Weather?> GetFullWeather(double latitude, double longitude);
}