using System.Text.Json;
using System.Text.RegularExpressions;

namespace Weather_Network.HelperUtils;

public class PascalCaseWithNumberPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        string pattern = @"([A-Z][a-z]+)|([0-9]+[a-z])";

        var splitedPropertyName = Regex.Matches(name, pattern);

        string snakeCaseName = string.Empty;

        if (splitedPropertyName.Count == 1)
        {
            snakeCaseName += splitedPropertyName[0];

            return snakeCaseName.ToLower();
        }

        foreach (var word in splitedPropertyName)
        {
            if (word == splitedPropertyName[0] || word != splitedPropertyName[^1])
                snakeCaseName += word + "_";

            else
                snakeCaseName += word;
        }

        return snakeCaseName.ToLower();
    }
}
