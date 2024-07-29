namespace WeatherNetwork.Models.Base;

public class HourlyWeather
{
    public ICollection<string>? Time { get; set; }
    public ICollection<double>? Temperature2m { get; set; }
    public ICollection<int>? RelativeHumidity2m { get; set; }
    public ICollection<int>? PrecipitationProbability { get; set; }
    public IList<int>? WeatherCode { get; set; }
    public ICollection<double>? WindSpeed80m { get; set; }
    public ICollection<int>? WindDirection80m { get; set; }
    public ICollection<double>? UvIndex { get; set; }
}
