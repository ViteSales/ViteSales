using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.SDK.Interfaces;

public interface IModule
{
    public string Name { get; }
    public string ModuleId { get; }
    public IEnumerable<Type> Entities { get; }
    
    public void OnLoad();
    public void OnChangeEvent(EventChange data);
}