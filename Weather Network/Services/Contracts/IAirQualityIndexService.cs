using WeatherNetwork.Models.Base;

namespace WeatherNetwork.Services.Contracts;

public interface IAirQualityIndexService
{
    public Task<AirQualityIndex?> GetAirQualityIndex(double latitude, double longitude);
}
