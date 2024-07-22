using System.Text.RegularExpressions;

namespace FSSEstate.Business.Implementations.Helpers;

public static class SeoUrlHelper
{
    public static string ToSeoUrl(string text)
    {
        // Convert to lowercase
        text = text.ToLowerInvariant();

        // Remove invalid characters
        text = Regex.Replace(text, @"[^a-z0-9\s-]", "");

        // Replace spaces and hyphens with a single hyphen
        text = Regex.Replace(text, @"[\s-]+", " ").Trim();
        text = Regex.Replace(text, @"\s", "-");

        // Remove leading and trailing hyphens
        text = text.Trim('-');

        return text;
    }
}
