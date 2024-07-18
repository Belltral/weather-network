namespace Weather_Network.Models;

public class WeatherViewModel
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double GenerationtimeMs { get; set; }
    public int UtcOffsetSeconds { get; set; }
    public string? Timezone { get; set; }
    public string? TimezoneAbbreviation { get; set; }
    public double Elevation { get; set; }

    public Dictionary<string, string>? CurrentUnits { get; set; }  
    public CurrentWeatherViewModel? Current { get; set; }

    public Dictionary<string, string>? HourlyUnits { get; set; }
    public HourlyWeatherViewModel? Hourly { get; set; }

    public Dictionary<string, string>? DailyUnits { get; set; }
    public DailyWeatherViewModel? Daily { get; set; }
}
