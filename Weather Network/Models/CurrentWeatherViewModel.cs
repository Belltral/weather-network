namespace WeatherNetwork.Models;

public class CurrentWeatherViewModel
{
    public string? Time { get; set; }
    public int Interval { get; set; }
    public string? Temperature2m { get; set; } //
    public int RelativeHumidity2m { get; set; }
    public string? ApparentTemperature { get; set; }
    public int IsDay { get; set; }
    public string? Precipitation { get; set; }
    public int WeatherCode { get; set; }
    public string? WeatherCondition { get; set; }
    public string? WindSpeed10m { get; set; }
    public int WindDirection10m { get; set; }
}
