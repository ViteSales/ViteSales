using ViteSales.ERP.Console.SamplePackage.Entities;
using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.Console.SamplePackage.Modules;

public class InvoiceModule:IModule
{
    public string Name { get; } = "Sales Invoice";
    public IEnumerable<Type> Entities { get; } = new List<Type>()
    {
        typeof(Invoice),
        typeof(InvoiceItems)
    };
    
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

    public List<object> DefaultValues { get; }
}