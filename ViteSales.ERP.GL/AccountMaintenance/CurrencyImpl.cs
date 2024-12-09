using System.Data;
using ViteSales.Data.Contracts;
using ViteSales.Data.Entities;
using ViteSales.Data.Extensions;

namespace ViteSales.ERP.GL.AccountMaintenance;

public class CurrencyImpl(IViteSalesDataContext ctx)
{
    public List<Currency>? Load()
    {
        var dt = ctx.Resource.Currencies.ToList();
        return dt.Count == 0 ? null : dt;
    }

    public void Save(DataTable dt)
    {
        using var transaction = ctx.Resource.Database.BeginTransaction();
        try
        {
            var rowsToDelete = dt.Rows.Cast<DataRow>()
                .Where(row => row.RowState == DataRowState.Deleted)
                .ToList(); 
            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    ctx.Resource.Currencies.Add(row.MapToEntity<Currency>());
                }

                if (row.RowState == DataRowState.Modified)
                {
                    var currency = ctx.Resource.Currencies
                        .FirstOrDefault(x => x.CurrencyCode == row["CurrencyCode"].ToString());
                    currency?.UpdateFromDataRow(row);
                }
            }

            foreach (var currencyToDelete in rowsToDelete
                         .Select(row => row["CurrencyCode", DataRowVersion.Original].ToString()).Select(currKey => ctx
                             .Resource.Currencies
                             .FirstOrDefault(x => x.CurrencyCode == currKey)).OfType<Currency>())
            {
                ctx.Resource.Currencies.Remove(currencyToDelete);
            }
            ctx.Resource.SaveChanges();
            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }
    }
}