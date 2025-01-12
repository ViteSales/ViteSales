using System.Data;
using ViteSales.ERP.Accounting.GL.Entities;
using ViteSales.ERP.Accounting.GL.Interfaces;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.Accounting.GL.Modules;

public class FiscalYearsModule: IFiscalYears
{
    public string Name { get; } = "Fiscal Years";

    public IEnumerable<Type> Entities { get; } = new List<Type>
    {
        typeof(FiscalYears)
    };
    
    public void OnLoad()
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