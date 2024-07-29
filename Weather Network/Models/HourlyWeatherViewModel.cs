namespace WeatherNetwork.Models;

public class HourlyWeatherViewModel
{
    public IList<DateTime>? Time { get; set; }
    public IList<string>? Temperature2m { get; set; }
    public IList<int>? RelativeHumidity2m { get; set; }
    public IList<int>? PrecipitationProbability { get; set; }
    public IList<string>? WeatherCode { get; set; }
    public IList<string>? WindSpeed80m { get; set; }
    public IList<int>? WindDirection80m { get; set; }
    public IList<string>? UvIndex { get; set; }
}
