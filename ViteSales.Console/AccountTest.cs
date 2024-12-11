using System.Data;
using ViteSales.Data.Entities;
using ViteSales.Data.Extensions;
using ViteSales.ERP.GL.Maintenance.Account;

namespace ViteSales.Console;

public class AccountTest(ConnectionProvider connectionProvider)
{
    private readonly AccountImpl _accType = new (connectionProvider);

    public void NormalAccountTest()
    {
        var m = _accType.GetDefaultGlMast();
        m.AccNo = "200-1000";
        m.AccType = "FA";
        m.Description = "Test Account";
        m.Siccode = "4001";
        var l = new List<Glmast>() { m };
        var d = l.ToDataTable();
        
        _accType.SaveNormalAccount(d);
    }

    public void DeleteNormalAccount()
    {
        _accType.DeleteAccount("200-1000");
    }
}