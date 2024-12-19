using ViteSales.ERP.Console.SamplePackage.Entities;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.Console.SamplePackage.Modules;

public class InvoiceModule: IModule
{
    public string Name { get; } = "Sales Invoice";
    public string ModuleId { get; } = "com.vite-sales.sales.invoice";
    public IEnumerable<Type> Entities { get; } = new List<Type>()
    {
        typeof(Invoice),
        typeof(InvoiceItems)
    };
    
    public void OnLoad()
    {
        throw new NotImplementedException();
    }

    public void OnChangeEvent(EventChange data)
    {
        throw new NotImplementedException();
    }
}