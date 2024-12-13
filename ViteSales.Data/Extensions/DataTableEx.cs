using System.Data;
using System.Reflection;
using ViteSales.Data.Contracts;

namespace ViteSales.Data.Extensions;

public static class DataTableEx
{
    public static string ToXml(this DataTable dt)
    {
        using var sw = new StringWriter();
        dt.WriteXml(sw);
        return sw.ToString();
    }
    
    public static List<T> ToList<T>(this DataTable table) where T : class, new()
    {
        var list = new List<T>();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (DataRow row in table.Rows)
        {
            var obj = new T();
            foreach (var prop in properties)
            {
                if (!table.Columns.Contains(prop.Name) || row[prop.Name] == DBNull.Value) continue;
                var safeValue = Convert.ChangeType(row[prop.Name], prop.PropertyType);
                prop.SetValue(obj, safeValue, null);
            }
            list.Add(obj);
        }
        return list;
    }
}