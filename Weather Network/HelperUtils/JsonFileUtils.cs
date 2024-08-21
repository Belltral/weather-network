using System.Text.Json;
using WeatherNetwork.WMOCodes;

namespace WeatherNetwork.HelperUtils;

public class JsonFileUtils
{
    private const string wmoCodesFile = @".\WMOCodes\WMOCodesDescription.json";
    private const string localizationsFile = @".\DefaultLocations.json";

    public static string? WMOCodeConverter(int wmoCode, string languageCode)
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

    public static Dictionary<string, string>? DefaultCoordinates(string localization)
    {
        using (StreamReader streamReader = new StreamReader(localizationsFile))
        {
            using (JsonDocument document = JsonDocument.Parse(streamReader.ReadToEnd()))
            {
                Dictionary<string, string> coordinates = [];

                JsonElement root = document.RootElement;
                bool localizationExists = root.TryGetProperty(localization, out JsonElement foundLocalization);

                if (localizationExists)
                {
                    coordinates.Add("latitude", foundLocalization.GetProperty("latitude").ToString());
                    coordinates.Add("longitude", foundLocalization.GetProperty("longitude").ToString());

                    return coordinates;
                }

                return null;
                 
            }
        }
    }
}
