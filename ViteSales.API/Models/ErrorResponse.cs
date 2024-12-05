using System.Collections;
using System.Text;
using System.Text.Json;

namespace ViteSales.API.Models;

[Serializable]
public class ErrorResponse
{
    public string? Stack { get; set; } = null;
    public string Message { get; set; } = "";
    public IDictionary? Data { get; set; } = null;
    public bool Status { get; set; } = false;

    public byte[] ToBytes()
    {
        return Encoding.ASCII.GetBytes(ToString() ?? string.Empty);
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}