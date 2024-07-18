namespace Weather_Network.Models;

public class DailyWeatherViewModel
{
    public List<string>? Time { get; set; }
    public List<double>? Temperature2mMax { get; set; }
    public List<double>? Temperature2mMin { get; set; }
    public List<string>? Sunrise { get; set; }
    public List<string>? Sunset { get; set; }
    public List<double>? DaylightDuration { get; set; }
    public List<double>? SunshineDuration { get; set; }
    public List<double>? UvIndexMax { get; set; }
    public List<double>? PrecipitationSum { get; set; }
    public List<double>? PrecipitationHours { get; set; }
    public List<int>? PrecipitationProbabilityMax { get; set; }
    public List<double>? WindSpeed10mMax { get; set; }
    public List<int>? WindDirection10mDominant { get; set; }
}