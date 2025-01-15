using System.Data;
using Microsoft.Extensions.DependencyInjection;
using ViteSales.ERP.SDK.Const;
using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.SDK.Interfaces;

public interface IModule
{
    public void OnLoad(ServiceProvider provider);
    public void OnChangeEvent(EventChange data);
    public Task OnSave(DataTable dataTable);
}