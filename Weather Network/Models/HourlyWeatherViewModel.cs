namespace Weather_Network.Models;

public class HourlyWeatherViewModel
{
    public List<string>? Time { get; set; }
    public List<double>? Temperature2m { get; set; }
    public List<int>? RelativeHumidity2m { get; set; }
    public List<int>? PrecipitationProbability { get; set; }
    public List<double>? WindSpeed80m { get; set; }
    public List<int>? WindDirection80m { get; set; }
}
