using Microsoft.Extensions.DependencyInjection;
using ViteSales.Console;
using ViteSales.Data.Entities;
using ViteSales.ERP.GL.Settings;

var dbProvider = new DatabaseProvider(true).Get();
using var ctx = dbProvider.GetRequiredService<ViteSalesContext>();

var connProvider = new ConnectionProvider(ctx);

#region Settings

var settings = new SettingsImpl(connProvider);
settings.CreateDefaults();

#endregion

#region AccType

var accTypeTest = new AccTypeTest(connProvider);
accTypeTest.TestEdit();
accTypeTest.TestDelete();

#endregion

#region Account

var accTest = new AccountTest(connProvider);
accTest.NormalAccountTest();
accTest.DeleteNormalAccount();
#endregion