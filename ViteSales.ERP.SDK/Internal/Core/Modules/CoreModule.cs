using System.Data;
using Microsoft.Extensions.DependencyInjection;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Internal.Core.Entities;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.SDK.Internal.Core.Modules;

[ModuleName("CoreModule")]
[ModuleEntities(typeof(PackageAuthorsInternal), typeof(PackageDetailsInternal), typeof(PackageInfoInternal), typeof(AuditTrailInternal))]
public class CoreModule: IModule
{
    public void OnLoad(ServiceProvider provider)
    {
        
    }

    public void OnChangeEvent(EventChange data)
    {
        throw new NotImplementedException();
    }

    public Task OnSave(DataTable dataTable)
    {
        throw new NotImplementedException();
    }

    public List<object> DefaultValues()
    {
        return [];
    }
}