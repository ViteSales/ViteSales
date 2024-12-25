using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Internal.Core.Entities;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.SDK.Internal.Core.Modules;

public class CoreModule: IModule
{
    public string Name { get; } = "CoreModule";

    public IEnumerable<Type> Entities { get; } = new List<Type>()
    {
        typeof(PackageAuthorsInternal),
        typeof(PackageDetailsInternal),
        typeof(PackageInfoInternal),
        typeof(AuditTrailInternal)
    };

    public IEnumerable<Type> ToStream { get; } = new List<Type>();

    public void OnLoad(DbContext ctx)
    {
        
    }

    public void OnChangeEvent(EventChange data)
    {
        throw new NotImplementedException();
    }

    public Task OnSave(List<object> records)
    {
        throw new NotImplementedException();
    }

    public List<object> DefaultValues()
    {
        return [];
    }
}