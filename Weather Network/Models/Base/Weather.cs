namespace WeatherNetwork.Models.Base;

public class Weather
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double GenerationtimeMs { get; set; }
    public int UtcOffsetSeconds { get; set; }
    public string? Timezone { get; set; }
    public string? TimezoneAbbreviation { get; set; }
    public double Elevation { get; set; }

    public Dictionary<string, string>? CurrentUnits { get; set; }
    public CurrentWeather? Current { get; set; }

    public Dictionary<string, string>? HourlyUnits { get; set; }
    public HourlyWeather? Hourly { get; set; }

    public Dictionary<string, string>? DailyUnits { get; set; }
    public DailyWeather? Daily { get; set; }
}
