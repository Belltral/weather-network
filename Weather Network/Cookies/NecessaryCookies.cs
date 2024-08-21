using System.Globalization;
using WeatherNetwork.Services.Contracts;

namespace WeatherNetwork.Cookies;

public class NecessaryCookies
{
    private readonly ICookiesHandlerService _cookiesHandler;
    private readonly HttpContext _httpContext;

    public NecessaryCookies(ICookiesHandlerService cookiesHandler, HttpContext httpContext)
    {
        _cookiesHandler = cookiesHandler;
        _httpContext = httpContext;
    }

    public string? Culture
    {
        get { return _cookiesHandler.CultureRequest(_httpContext); }
    }

    public string? Language
    {
        get { return String.IsNullOrEmpty(Culture) ? "pt" : Culture[..2]; }
    }

    public string? Localization
    {
        get { return String.IsNullOrEmpty(Culture) ? "BR" : Culture[3..]; }
    }

    public double Latitude
    {
        get 
        {
            var latitude = _cookiesHandler.CoordinatesRequest(_httpContext, Localization!);
            return double.Parse(latitude!["latitude"], CultureInfo.InvariantCulture);
        }
    }

    public double Longitude
    {
        get
        {
            var longitude = _cookiesHandler.CoordinatesRequest(_httpContext, Localization!);
            return double.Parse(longitude!["longitude"], CultureInfo.InvariantCulture);
        }
    }
}
