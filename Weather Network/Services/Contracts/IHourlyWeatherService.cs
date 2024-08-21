using WeatherNetwork.Models.Base;

namespace WeatherNetwork.Services.Contracts;

public interface IHourlyWeatherService
{
    public Task<HourlyWeather?> GetHourly(double latitude, double longitude);
}
