namespace WeatherNetwork.Models;

public class DailyWeatherViewModel
{
    public string? CityCountry { get; set; }
    public IList<DateTime>? Time { get; set; }
    public IList<int>? WeatherCode { get; set; }
    public IList<string>? WeatherCondition { get; set; }
    public IList<string>? Temperature2mMax { get; set; }
    public IList<string>? Temperature2mMin { get; set; }
    public IList<string>? Sunrise { get; set; }
    public IList<string>? Sunset { get; set; }
    public IList<int>? DaylightDuration { get; set; }
    public IList<int>? SunshineDuration { get; set; }
    public IList<string>? UvIndexMax { get; set; }
    public IList<string>? PrecipitationSum { get; set; }
    public IList<int>? PrecipitationProbabilityMax { get; set; }
    public IList<string>? WindSpeed10mMax { get; set; }
    public IList<int>? WindDirection10mDominant { get; set; }
}