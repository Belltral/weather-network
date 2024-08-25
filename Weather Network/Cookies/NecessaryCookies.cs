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
        get 
        { 
            var culture = _cookiesHandler.CultureRequest(_httpContext);
            return String.IsNullOrEmpty(culture) ? "pt-BR" : culture;
        }
    }

    public string? Language
    {
        get { return Culture![..2]; }
    }

    public Dictionary<string, string>? Localization
    {
        get { return _cookiesHandler.LocalizationRequest(_httpContext, Culture!); }
    }

    public double Latitude
    {
        get { return double.Parse(Localization!["latitude"], CultureInfo.InvariantCulture); }
    }

    public double Longitude
    {
        get { return double.Parse(Localization!["longitude"], CultureInfo.InvariantCulture); }
    }

    public string? City
    {
        get { return Localization!["city"]; }
    }

    public string? Country
    {
        get { return Localization!["country"]; }
    }

    public void SaveLocalization(double latitude, double longitude, string city, string country)
    {
        _cookiesHandler.AppendCookie(_httpContext, "latitude", latitude.ToString(CultureInfo.InvariantCulture),
                new CookieOptions { Expires = DateTime.Now.AddDays(15) }, true);

        _cookiesHandler.AppendCookie(_httpContext, "longitude", longitude.ToString(CultureInfo.InvariantCulture),
            new CookieOptions { Expires = DateTime.Now.AddDays(15) }, true);

        _cookiesHandler.AppendCookie(_httpContext, "city", city,
            new CookieOptions { Expires = DateTime.Now.AddDays(15) }, true);

        _cookiesHandler.AppendCookie(_httpContext, "country", country,
            new CookieOptions { Expires = DateTime.Now.AddDays(15) }, true);
    }
}
