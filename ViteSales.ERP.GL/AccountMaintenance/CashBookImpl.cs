using Microsoft.EntityFrameworkCore;
using ViteSales.Data.Contracts;

namespace ViteSales.ERP.GL.AccountMaintenance;

public class CashBookImpl(IViteSalesDataContext ctx)
{
    public void DeleteCashbook(long docKey)
    {
        using var transaction = ctx.Resource.Database.BeginTransaction();
        try
        {
            ctx.Resource.Cbs
                .Where(row => row.DocKey == docKey)
                .ExecuteDelete();
            ctx.Resource.Cbdtls
                .Where(row => row.DocKey == docKey)
                .ExecuteDelete();
            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }
    }
}