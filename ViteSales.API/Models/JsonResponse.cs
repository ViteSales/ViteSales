namespace ViteSales.API.Models;

[Serializable]
public class JsonResponse<T> where T : class
{
    public string Message { get; set; } = "";
    public T? Data { get; set; } = null;
    public bool Status { get; set; } = false;
}