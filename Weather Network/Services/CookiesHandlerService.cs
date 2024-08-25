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

    public Dictionary<string, string>? LocalizationRequest(HttpContext httpContext, string culture)
    {
        IList<string> cookiesName = ["latitude", "longitude", "city", "country"];

        Dictionary<string, string> coordinates = [];

        foreach (var name in cookiesName)
        {
            coordinates.Add(name, httpContext.Request.Cookies[name]);
        }

        if (String.IsNullOrEmpty(coordinates["latitude"]) && String.IsNullOrEmpty(coordinates["longitude"]))
        {
            var defaultCoordinates = JsonFileUtils.DefaultLocalization(culture);
            coordinates.Clear();

            foreach (var name in cookiesName)
            {
                httpContext.Response.Cookies.Append(name, defaultCoordinates![name], new CookieOptions { Expires = DateTime.Now.AddDays(15)});
                coordinates.Add(name, defaultCoordinates![name]);
            }

            return coordinates;
        }

        return coordinates;
    }
}