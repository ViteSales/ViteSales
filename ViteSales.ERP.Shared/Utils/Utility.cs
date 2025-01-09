namespace ViteSales.ERP.Shared.Utils;

public static class Utility
{
    public static string GetUniqueId()
    {
        var uniqueAlphabets = new HashSet<char>();
        var random = new Random();
        while (uniqueAlphabets.Count < 26)
        {
            var letter = (char)random.Next('A', 'Z' + 1);
            uniqueAlphabets.Add(letter);
        }
        return new string(uniqueAlphabets.ToArray());
    }

    public static string GetSubscriberName(string host, string db, string table)
    {
        return new string($"{host}{db}{table}".Where(char.IsLetterOrDigit).ToArray()).ToLower();
    }

    public static string GetTopicName(string host, string db)
    {
        return new string($"{host}{db}".Where(char.IsLetterOrDigit).ToArray()).ToLower();
    }
}