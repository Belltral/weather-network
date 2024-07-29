using System.Text.Json;
using WeatherNetwork.WMOCodes;

namespace WeatherNetwork.HelperUtils;

public class WMOCodeConverter
{
    private const string wmoCodesFile = @".\WMOCodes\WMOCodesDescription.json";

    public static string? Converter(int wmoCode, string languageCode)
    {
        var wmoCondition = ((WMOCode)wmoCode).ToString();

        using (StreamReader streamReader = new StreamReader(wmoCodesFile))
        {
            using (JsonDocument document = JsonDocument.Parse(streamReader.ReadToEnd()))
            {
                JsonElement root = document.RootElement;
                JsonElement condition = root.GetProperty(wmoCondition);

                bool descriptionExists = condition.TryGetProperty(languageCode, out JsonElement description);

                if (descriptionExists)
                    return description.ToString();

                return null;
            }
        }
    }
}
