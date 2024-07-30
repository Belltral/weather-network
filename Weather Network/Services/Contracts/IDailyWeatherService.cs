using WeatherNetwork.Models.Base;

namespace WeatherNetwork.Services.Contracts;

public interface IDailyWeatherService
{
    public Task<DailyWeather?> GetDailyWeather(double latitude, double longitude);
}
