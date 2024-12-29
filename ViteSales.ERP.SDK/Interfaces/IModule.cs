using ViteSales.ERP.SDK.Const;
using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.SDK.Interfaces;

public interface IModule
{
    public string Name { get; }
    public IEnumerable<Type> Entities { get; }
    
    public void OnLoad(DbContext ctx);
    public void OnChangeEvent(EventChange data);
    public Task OnSave(List<object> records);
    public List<object> DefaultValues();
}