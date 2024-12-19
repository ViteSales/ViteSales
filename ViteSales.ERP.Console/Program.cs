using ViteSales.ERP.Console.SamplePackage;
using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Models;

var manifest = new Manifest();
var conn = new Connection(new ConnectionConfig()
{
    Host = "localhost",
    Port = 5432,
    User = "postgres",
    Password = "frf@!333!@Fg",
    Database = "postgres"
});
var manager = new TableSchemaManager(conn);
manager.CreateOrUpdateTablesAsync(manifest.Modules.SelectMany(x => x.Entities)).Wait();