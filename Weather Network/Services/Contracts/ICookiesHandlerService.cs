namespace WeatherNetwork.Services.Contracts;

public interface ICookiesHandlerService
{
    public void AppendCookie(HttpContext context, string key, string value, CookieOptions? cookieOptions, bool overwrite);
    public string? CultureRequest(HttpContext context);
    public Dictionary<string, string>? CoordinatesRequest(HttpContext context, string localization);
}
