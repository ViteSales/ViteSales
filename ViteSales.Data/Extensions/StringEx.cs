namespace ViteSales.Data.Extensions;

public static class StringEx
{
    public static string ToSnakeCase(this string str) => 
        string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
    
    public static string ToCamelCase(this string str) =>
        string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? char.ToLower(x).ToString() : x.ToString()));
    
    public static string ToPascalCase(this string str) =>
        string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? char.ToUpper(x).ToString() : x.ToString()));
}