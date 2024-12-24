namespace ViteSales.ERP.SDK.Utils;

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
}