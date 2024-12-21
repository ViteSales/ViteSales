using SqlKata.Execution;
using ViteSales.ERP.Console.SamplePackage;
using ViteSales.ERP.Console.SamplePackage.Entities;
using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Database.Operation;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Models;
using ViteSales.ERP.SDK.Validator;

var manifest = new Manifest();
var conn = new Connection(new ConnectionConfig()
{
    Host = "localhost",
    Port = 5432,
    User = "postgres",
    Password = "frf@!333!@Fg",
    Database = "postgres"
});
await conn.OpenConnectionAsync();

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
var res = new FormValidator<Invoice>().Validate(invoice);
Console.WriteLine(res.IsValid ? "Valid" : "Invalid");
if (res.IsValid)
{
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
    ]);
}