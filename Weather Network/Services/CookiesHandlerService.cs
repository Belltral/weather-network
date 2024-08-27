using Microsoft.AspNetCore.Localization;
using WeatherNetwork.Cookies;
using WeatherNetwork.HelperUtils;
using WeatherNetwork.Services.Contracts;

namespace WeatherNetwork.Services;

public class CookiesHandlerService : ICookiesHandlerService
{
    public void AppendCookie(HttpContext httpContext, Cookie cookie, bool overwrite = true)
    {
        var findCookie = httpContext.Request.Cookies[cookie.Key!];

        if (!String.IsNullOrEmpty(findCookie) && !overwrite)
            return;

        if (cookie.Options is null)
        {
            httpContext.Response.Cookies.Append(cookie.Key!, cookie.Value!);
            return;
        }

        httpContext.Response.Cookies.Append(cookie.Key!, cookie.Value!, cookie.Options);

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
                httpContext.Response.Cookies.Append(name, defaultCoordinates![name], new CookieOptions { Expires = DateTime.Now.AddDays(15) });
                coordinates.Add(name, defaultCoordinates![name]);
            }

            return coordinates;
        }

        return coordinates;
    }
}