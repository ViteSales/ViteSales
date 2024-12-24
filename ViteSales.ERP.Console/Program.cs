using ViteSales.ERP.Console.SamplePackage;
using ViteSales.ERP.Console.SamplePackage.Entities;
using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Database.Operation;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Internal.Core.Repositories;
using ViteSales.ERP.SDK.Models;

var manifest = new Manifest();
var conn = new ConnectionConfig()
{
    Host = "localhost",
    Port = 5432,
    User = "postgres",
    Password = "frf@!333!@Fg",
    Database = "postgres"
};

var pkg = new PackageInfo(conn);
await pkg.Install(new ViteSales.ERP.SDK.Internal.Core.Manifest());
// await pkg.Uninstall(new ViteSales.ERP.SDK.Internal.Core.Manifest());

/*
var db = new DbContext(conn);
var manager = new TableSchemaManager(conn);
await manager.CreateOrUpdateTablesAsync(manifest.Modules.SelectMany(x => x.Entities));

var invoice = new Invoice()
{
    Id = Guid.NewGuid(),
    DocNo = "1001",
    DocNoSet = "D1001",
    DebtorCode = "A1"
};
await db.SaveChanges(() =>[
    new Insert<Invoice>(invoice),
    new Update<Invoice>(invoice,[new WhereClause()
    {
        Field = "Id",
        Operator = "=",
        Value = invoice.Id
    }]),
    new Delete<Invoice>([new WhereClause()
    {
        Field = "Id",
        Operator = "=",
        Value = invoice.Id
    }])
]);*/