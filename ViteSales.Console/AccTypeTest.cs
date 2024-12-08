using System.Data;
using ViteSales.Data.Extensions;
using ViteSales.ERP.GL.AccountMaintenance;

namespace ViteSales.Console;

public class AccTypeTest(ConnectionProvider connectionProvider)
{
    private readonly AccTypeImpl _accType = new (connectionProvider);
    
    public void TestAdd(int numOfRows = 1)
    {
        var dt = TestGet();
        if (dt is null)
        {
            dt = new DataTable();
            dt.Columns.Add("AccTypeCode");
            dt.Columns.Add("Description");
            dt.Columns.Add("Desc2");
            dt.Columns.Add("IsBstype");
            dt.Columns.Add("IsSystemType");
        }
        for (var i = 0; i < numOfRows; i++)
        {
            var newRow = dt.NewRow();
            newRow["AccTypeCode"] = $"{i + 1}";
            newRow["Description"] = $"New Type {i + 1}";
            newRow["Desc2"] = $"Description 2 for New Type {i + 1}";
            newRow["IsBstype"] = "T";
            newRow["IsSystemType"] = "F";
            dt.Rows.Add(newRow);
        }
        _accType.Save(dt);
    }
    
    public void TestEdit()
    {
        var dt = TestGet();
        if (dt.Rows.Count > 0)
        {
            var existingRow = dt.Rows[0];
            existingRow["Description"] = "Description for Existing Type";
            existingRow["Desc2"] = "Description 2 for Existing Type";
            
            _accType.Save(dt);
        }
        else
        {
            System.Console.WriteLine("No rows found to edit");
        }
    }
    
    public void TestDelete()
    {
        var dt = TestGet();
        if (dt.Rows.Count > 0)
        {
            var rowToDelete = dt.Rows[0];
            rowToDelete.Delete();
            
            _accType.Save(dt);
        }
        else
        {
            System.Console.WriteLine("No rows found to delete");
        }
    }
    
    public DataTable? TestGet()
    {
        return _accType.Load()?.ToDataTable("AccTypeCode");
    }
}