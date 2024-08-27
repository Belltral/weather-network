
namespace WeatherNetwork.Cookies
{
    public class Cookie
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
        public CookieOptions? Options { get; set; }

        public Cookie() { }

        public Cookie(string? key, string? value, CookieOptions? options)
        {
            Key = key;
            Value = value;
            Options = options;
        }
    }
}
