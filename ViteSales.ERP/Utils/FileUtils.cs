using Microsoft.JSInterop;

namespace ViteSales.ERP.Utils;

public static class FileUtils
{
    public static ValueTask<object> SaveAs(this IJSRuntime js, string fileName, byte[] data)
    => js.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(data));
}