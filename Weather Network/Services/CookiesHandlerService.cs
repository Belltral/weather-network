using Microsoft.AspNetCore.Localization;
using WeatherNetwork.HelperUtils;
using WeatherNetwork.Services.Contracts;

namespace WeatherNetwork.Services;

public class CookiesHandlerService : ICookiesHandlerService
{
    public void AppendCookie(HttpContext httpContext, string key, string value, CookieOptions? cookieOptions, bool overwrite = true)
    {
        var findCookie = httpContext.Request.Cookies[key];

        if (!String.IsNullOrEmpty(findCookie) && !overwrite)
            return;

        if (cookieOptions is null)
        {
            httpContext.Response.Cookies.Append(key, value);
            return;
        }

        httpContext.Response.Cookies.Append(key, value, cookieOptions);
    }

    public string? CultureRequest(HttpContext httpContext)
    {
        string? cultureCookie = httpContext.Request.Cookies["culture"];
        string? culture;

        if (String.IsNullOrEmpty(cultureCookie))
        {
            var requestCulture = httpContext.Request.HttpContext.Features.Get<IRequestCultureFeature>();
            culture = requestCulture!.RequestCulture.Culture.ToString();
            httpContext.Response.Cookies.Append("culture", culture);

            return culture;
        }

        culture = cultureCookie;

        return culture;
    }

    public Dictionary<string, string>? CoordinatesRequest(HttpContext httpContext, string localization)
    {
        string? latitude = httpContext.Request.Cookies["latitude"];
        string? longitude = httpContext.Request.Cookies["longitude"];

        Dictionary<string, string> coordinates = [];

        if (String.IsNullOrEmpty(latitude) && String.IsNullOrEmpty(longitude))
        {
            var defaultCoordinates = JsonFileUtils.DefaultCoordinates(localization);

            string defaultLatitude = defaultCoordinates!["latitude"];
            string defaultLongitude = defaultCoordinates!["longitude"];

            httpContext.Response.Cookies.Append("latitude", defaultLatitude);
            httpContext.Response.Cookies.Append("longitude", defaultLongitude);

            coordinates.Add("latitude", defaultLatitude);
            coordinates.Add("longitude", defaultLongitude);

            return coordinates;
        }

        coordinates.Add("latitude", latitude!);
        coordinates.Add("longitude", longitude!);

        return coordinates;
    }
}