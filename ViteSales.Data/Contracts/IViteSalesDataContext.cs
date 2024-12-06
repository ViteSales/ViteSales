using System.Data;
using ViteSales.Data.Entities;

namespace ViteSales.Data.Contracts;

public interface IViteSalesDataContext
{
    public string Username { get; set; }
    public ViteSalesContext Resource { get; set; }
    public void OnAdded(DataTable dt);
    public void OnUpdated(DataTable dt);
    public void OnDeleted(DataTable dt);
}