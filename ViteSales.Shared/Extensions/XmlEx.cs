using System.Xml.Serialization;

namespace ViteSales.Shared.Extensions;

public static class XmlEx
{
    public static string ToXml<T>(T obj)
    {
        var serializer = new XmlSerializer(typeof(T));
        using var stringWriter = new StringWriter();
        serializer.Serialize(stringWriter, obj);
        return stringWriter.ToString();
    }
}