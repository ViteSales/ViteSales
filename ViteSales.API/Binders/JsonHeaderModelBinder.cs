using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ViteSales.API.Binders;

public class JsonHeaderModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var headerValue = bindingContext.HttpContext.Request.Headers[bindingContext.FieldName].ToString();
        if (string.IsNullOrEmpty(headerValue))
        {
            return Task.CompletedTask;
        }
        var model = JsonSerializer.Deserialize(headerValue, bindingContext.ModelType);
        bindingContext.Result = ModelBindingResult.Success(model);
        return Task.CompletedTask;
    }
}