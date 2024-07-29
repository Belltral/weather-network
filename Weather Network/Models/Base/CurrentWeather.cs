namespace WeatherNetwork.Models.Base;

public class CurrentWeather
{
    public string? Time { get; set; }
    public int Interval { get; set; }
    public double Temperature2m { get; set; }
    public int RelativeHumidity2m { get; set; }
    public double ApparentTemperature { get; set; }
    public int IsDay { get; set; }
    public double Precipitation { get; set; }
    public int WeatherCode { get; set; }
    public double WindSpeed10m { get; set; }
    public int WindDirection10m { get; set; }
}
