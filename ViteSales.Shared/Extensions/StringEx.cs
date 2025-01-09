using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ViteSales.Shared.Extensions;

public static class StringEx
{
    public static string ToSnakeCase(this string str) => 
        string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
    
    public static string ToCamelCase(this string str) =>
        string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? char.ToLower(x).ToString() : x.ToString()));
    
    public static string ToPascalCase(this string str) =>
        string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? char.ToUpper(x).ToString() : x.ToString()));
    
    public static string ToKebabCase(this string str) =>
        string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + char.ToLower(x).ToString() : x.ToString()));
    
    public static string ToTitleCase(this string str) =>
        string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + char.ToUpper(x).ToString() : x.ToString()));
    
    public static string ToUpperFirst(this string str) =>
        char.ToUpper(str[0]) + str.Substring(1);
    
    public static string ToLowerFirst(this string str) =>
        char.ToLower(str[0]) + str.Substring(1);
    
    public static string ToSlug(this string phrase)
    {
        if (string.IsNullOrEmpty(phrase))
            return string.Empty;

        // Convert to lowercase
        string str = phrase.ToLowerInvariant().RemoveDiacritics();

        // Replace invalid characters with empty string
        // Allow only lowercase letters, numbers, and spaces/hyphens
        str = Regex.Replace(str, @"[^a-z0-9\s-]", "");

        // Replace multiple spaces or hyphens with a single space
        str = Regex.Replace(str, @"[\s-]+", " ").Trim();

        // Replace spaces with hyphens
        str = Regex.Replace(str, @"\s", "-");

        return str;
    }

    /// <summary>
    /// Removes diacritics (accents) from the string.
    /// </summary>
    /// <param name="text">The input string.</param>
    /// <returns>The string without diacritics.</returns>
    public static string RemoveDiacritics(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
    
    // Optional: Ensure uniqueness by appending a unique identifier
    /// <summary>
    /// Generates a unique slug by appending a GUID.
    /// </summary>
    /// <param name="phrase">The input string to convert into a slug.</param>
    /// <returns>A unique URL-friendly slug.</returns>
    public static string ToSlugUnique(this string phrase)
    {
        var uniqueIdentifier = Guid.NewGuid().ToString().Substring(0, 8); // Short GUID
        return $"{phrase.ToSlug()}-{uniqueIdentifier}";
    }
}