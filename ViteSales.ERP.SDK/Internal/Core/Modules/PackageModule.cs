using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.SDK.Internal.Core.Modules;

public class PackageModule: IModule
{
    public string Name { get; }
    public IEnumerable<Type> Entities { get; }
    public void OnLoad(DbContext ctx)
    {
        throw new NotImplementedException();
    }

    public void OnChangeEvent(EventChange data)
    {
        throw new NotImplementedException();
    }

    public Task OnSave(List<object> records)
    {
        throw new NotImplementedException();
    }

    public void OnValidate(List<object> records)
    {
        throw new NotImplementedException();
    }
}