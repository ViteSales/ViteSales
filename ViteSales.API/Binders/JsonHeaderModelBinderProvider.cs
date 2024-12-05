using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ViteSales.API.Binders;

public class JsonHeaderModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        return context.BindingInfo.BindingSource == BindingSource.Header ? new JsonHeaderModelBinder() : null;
    }
}