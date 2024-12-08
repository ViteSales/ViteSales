using Microsoft.Extensions.DependencyInjection;
using ViteSales.Console;
using ViteSales.Data.Entities;

var dbProvider = new DatabaseProvider(true).Get();
using var ctx = dbProvider.GetRequiredService<ViteSalesContext>();

#region AccType

var accTypeTest = new AccTypeTest(new ConnectionProvider(ctx));
accTypeTest.TestAdd(10);
accTypeTest.TestEdit();
accTypeTest.TestDelete();



#endregion