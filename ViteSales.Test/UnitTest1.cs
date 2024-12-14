using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Models;
using ViteSales.Test.SamplePackage;

namespace ViteSales.Test;

public class Tests
{
    private Manifest _manifest = new Manifest();
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void Test1()
    {
        var conn = new Connection(new ConnectionConfig()
        {
            Host = "localhost",
            Port = 5432,
            User = "postgres",
            Password = "frf@!333!@Fg",
            Database = "postgres"
        });
        var manager = new TableSchemaManager(conn);
        manager.CreateOrUpdateTablesAsync(_manifest.Modules.SelectMany(x => x.Entities)).Wait();
        Assert.Pass();
    }
}