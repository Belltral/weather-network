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

        using StreamReader streamReader = new (wmoCodesFile);
        using JsonDocument document = JsonDocument.Parse(streamReader.ReadToEnd());

        JsonElement root = document.RootElement;
        JsonElement condition = root.GetProperty(wmoCondition);

        bool descriptionExists = condition.TryGetProperty(languageCode, out JsonElement description);

        if (descriptionExists)
            return description.ToString();

        return null;
    }

    public static Dictionary<string, string>? DefaultCoordinates(string localization)
    {
        using StreamReader streamReader = new (localizationsFile);
        using JsonDocument document = JsonDocument.Parse(streamReader.ReadToEnd());

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

    public static string? IconName(int wmoCode)
    {
        string imagesPath = @"D:\Estudos\Front-End\WeatherNetwork\images";

        int dayNightCode = 0;
        string dayNight = (dayNightCode == 0) ? "Day" : "Night";
        List<int> dayNightVariables = [0, 1, 2];
        string? wmoCondition = ((WMOCode)wmoCode).ToString();

        bool fileExists = File.Exists(imagesPath + @$"\{wmoCondition}.svg");

        if (fileExists)
        {
            return $"{wmoCondition}.svg";
        }

        else if (dayNightVariables.Contains(wmoCode) && File.Exists(imagesPath + @$"\{wmoCondition}{dayNight}.svg"))
        {
            return $"{wmoCondition}{dayNight}.svg";
        }

        if (wmoCondition == wmoCode.ToString())
            return null;

        using StreamReader streamReader = new(wmoCodesFile);
        using JsonDocument document = JsonDocument.Parse(streamReader.ReadToEnd());

        JsonElement root = document.RootElement;
        JsonElement condition = root.GetProperty(wmoCondition);
        JsonElement icon = condition.GetProperty("icon");

        return $"{icon}.svg";
    }
}
