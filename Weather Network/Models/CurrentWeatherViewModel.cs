namespace Weather_Network.Models;

public class CurrentWeatherViewModel
{
    public string? Time { get; set; }
    public int Interval { get; set; }
    public double Temperature2m { get; set; }
    public int RelativeHumidity2m { get; set; }
    public double ApparentTemperature { get; set; }
    public int IsDay { get; set; }
    public double Precipitation { get; set; }
    public double WindSpeed10m { get; set; }
    public int WindDirection10m { get; set; }
}
