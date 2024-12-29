using ViteSales.ERP.Accounting.GL.Entities;
using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.Accounting.GL.Modules;

public class FiscalYearsModule: IModule
{
    private DbContext _ctx;
    public string Name { get; } = "Fiscal Years";

    public IEnumerable<Type> Entities { get; } = new List<Type>
    {
        typeof(FiscalYears)
    };
    
    public void OnLoad(DbContext ctx)
    {
        _ctx = ctx;
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
        return
        [
            new FiscalYears
            {
                Id = Guid.NewGuid(),
                Name = "Fiscal Year " + DateTime.Now.Year,
                StartDate = DateTime.Now.AddYears(-1).Date,
                EndDate = DateTime.Now.Date,
                IsActive = true,
            }
        ];
    }
}