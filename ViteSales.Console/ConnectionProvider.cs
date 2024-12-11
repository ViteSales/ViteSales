using System;
using System.Data;
using ViteSales.Data.Contracts;
using ViteSales.Data.Entities;

namespace ViteSales.Console;

public class ConnectionProvider(ViteSalesContext ctx): IViteSalesDataContext
{
    public string Username { get; set; } = "SYSTEM::TESTING";
    public string DefaultProjectNo { get; set; } = "";
    public string DefaultDepartmentNo { get; set; } = "";
    public ViteSalesContext Resource { get; set; } = ctx;
    
    public void OnAdded(DataTable dt)
    {
        System.Console.WriteLine("OnAdded");
    }

    public void OnUpdated(DataTable dt)
    {
        System.Console.WriteLine("OnUpdated");
    }

    public void OnDeleted(DataTable dt)
    {
        System.Console.WriteLine("OnDeleted");
    }
}