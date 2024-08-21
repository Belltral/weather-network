namespace WeatherNetwork.Models;

public class TodayWeatherViewModel
{
    public CurrentWeatherViewModel? Current { get; set; }
    public HourlyWeatherViewModel? Hourly { get; set; }

    public string? Time { get; set; }
    public string? WeatherCode { get; set; }
    public string? Temperature2mMax { get; set; }
    public string? Temperature2mMin { get; set; }
    public string? Sunrise { get; set; }
    public string? Sunset { get; set; }
    public int DaylightDuration { get; set; }
    public int SunshineDuration { get; set; }
    public string? UvIndexMax { get; set; }
    public string? PrecipitationSum { get; set; }
    public int PrecipitationProbabilityMax { get; set; }
    public string? WindSpeed10mMax { get; set; }
    public int WindDirection10mDominant { get; set; }
}
