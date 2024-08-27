using WeatherNetwork.Cookies;

namespace WeatherNetwork.Services.Contracts;

public interface ICookiesHandlerService
{
    public void AppendCookie(HttpContext context, Cookie cookie, bool overwrite);
    public string? CultureRequest(HttpContext context);
    public Dictionary<string, string>? LocalizationRequest(HttpContext context, string localization);
}
