using System.Data;
using Microsoft.Extensions.DependencyInjection;
using ViteSales.ERP.Accounting.GL.Entities;
using ViteSales.ERP.Accounting.GL.Interfaces;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.Accounting.GL.Modules;

[ModuleName("Fiscal Years")]
[ModuleEntities(typeof(FiscalYears))]
public class FiscalYearsModule: IFiscalYears
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

    public static List<object> DefaultValues()
    {
        return
        [
            new FiscalYears
            {
                Id = Guid.NewGuid(),
                Name = "Fiscal Year " + DateTime.Now.Year,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.AddYears(1).Date,
                IsActive = true,
            }
        ];
    }
}