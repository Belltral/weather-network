using System.Text.Json.Serialization;

namespace WeatherNetwork.Models.Base;

public class CurrentAirQualityIndex
{
    public string? Time { get; set; }
    public int Interval { get; set; }
    public int UsAqi { get; set; }
    public double Pm10 { get; set; }

    [JsonPropertyName("pm2_5")]
    public double Pm25 { get; set; }
    public double CarbonMonoxide { get; set; }
    public double NitrogenDioxide { get; set; }
    public double SulphurDioxide { get; set; }
    public double Ozone { get; set; }
    public double UvIndex { get; set; }
}
