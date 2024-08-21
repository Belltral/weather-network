namespace WeatherNetwork.Models.Base;

public class DailyWeather
{
    public ICollection<string>? Time { get; set; }
    public IList<int>? WeatherCode { get; set; }
    public ICollection<double>? Temperature2mMax { get; set; }
    public ICollection<double>? Temperature2mMin { get; set; }
    public ICollection<string>? Sunrise { get; set; }
    public ICollection<string>? Sunset { get; set; }
    public ICollection<double>? DaylightDuration { get; set; }
    public ICollection<double>? SunshineDuration { get; set; }
    public ICollection<double>? UvIndexMax { get; set; }
    public ICollection<double>? PrecipitationSum { get; set; }
    public ICollection<int>? PrecipitationProbabilityMax { get; set; }
    public ICollection<double>? WindSpeed10mMax { get; set; }
    public ICollection<int>? WindDirection10mDominant { get; set; }
}
