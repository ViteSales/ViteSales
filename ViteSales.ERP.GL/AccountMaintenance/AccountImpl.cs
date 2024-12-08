using System.Data;
using Microsoft.EntityFrameworkCore;
using ViteSales.Data.Constants;
using ViteSales.Data.Contracts;
using ViteSales.Data.Entities;
using ViteSales.Data.Exceptions;
using ViteSales.Data.Extensions;
using ViteSales.Data.Utils;
using ViteSales.ERP.GL.Exceptions;

namespace ViteSales.ERP.GL.AccountMaintenance;

public class AccountImpl(IViteSalesDataContext ctx)
{
    #region DELETE

    public void DeleteNormalAccount(string accNo)
    {
        ctx.Resource.Glmasts.Where(row => row.AccNo == accNo).ExecuteDelete();
    }

    public void DeleteAccount(string accNo)
    {
        var fiscalYearImpl = new FiscalYearImpl(ctx);
        var firstRow = ctx.Resource.Glmasts.FirstOrDefault(row => row.AccNo == accNo);
        if (firstRow == null)
        {
            throw new MissingPropException<string>("Invalid 'Account No' provided",accNo);
        }
        var specialType = AccountTypes.ToSpecialAccountType(firstRow.SpecialAccType);
        var (startPeriod, _) = fiscalYearImpl.GetFiscalYearPeriod();
        using var transaction = ctx.Resource.Database.BeginTransaction();
        try
        {
            var fiscalStartDate = startPeriod.StartDate.ToString("yyyy-MM-dd");
            ctx.Resource.Obalances
                .Where(row => row.AccNo == accNo)
                .ExecuteDelete();
            ctx.Resource.Pbalances
                .Where(row => row.AccNo == accNo)
                .ExecuteDelete();
            ctx.Resource.Database
                .ExecuteSqlRaw("DELETE FROM TaxTransAuditDTL WHERE TaxTransKey NOT IN (Select TaxTransKey From TaxTrans) AND TaxDate < ? AND Action != 2 AND (TaxableAccNo = ? OR TaxAccNo = ?)",
                    fiscalStartDate, accNo, accNo);
            ctx.Resource.Database
                .ExecuteSqlRaw(
                    "DELETE TaxTransAuditDTL FROM TaxTransAuditDTL A WHERE A.TaxTransKey NOT IN (Select TaxTransKey From TaxTrans) AND A.TaxDate < ? AND A.Action = 3 AND Exists(SELECT 1 FROM TaxTransAuditDTL B WHERE B.Action = 2 and A.TaxTransAuditKey = B.TaxTransAuditKey and A.TaxTransKey = B.TaxTransKey AND (B.TaxableAccNo = ? OR B.TaxAccNo = ?))",
                    fiscalStartDate, accNo, accNo);
            ctx.Resource.Database
                .ExecuteSqlRaw(
                    "DELETE TaxTransAuditDTL FROM TaxTransAuditDTL A WHERE A.Action = 2 AND Not Exists(SELECT 1 FROM TaxTransAuditDTL B WHERE B.Action = 3 and A.TaxTransAuditKey = B.TaxTransAuditKey and A.TaxTransKey = B.TaxTransKey)");
            ctx.Resource.Database
                .ExecuteSqlRaw(
                    "DELETE FROM TaxTransAudit WHERE TaxTransAuditKey NOT IN (Select TaxTransAuditKey From TaxTransAuditDTL)");
            if (specialType is AccountTypes.SpecialAccountType.Bank or 
                AccountTypes.SpecialAccountType.Cash or 
                AccountTypes.SpecialAccountType.Deposit or 
                AccountTypes.SpecialAccountType.Others)
            {
                DeletePaymentMethod(accNo);
            }
            else if (specialType is AccountTypes.SpecialAccountType.FixedAsset)
            {
                DeleteFixedAssetAccount(accNo);
            }
            else if (specialType is AccountTypes.SpecialAccountType.AccumulatedDepreciation)
            {
                DeleteAccumulatedDepreciationAccount(accNo);
            }
            else if (specialType is AccountTypes.SpecialAccountType.OpenStock)
            {
                DeleteOpenStockAccount(accNo);
            }
            else if (specialType is AccountTypes.SpecialAccountType.CloseStock)
            {
                DeleteCloseStockAccount(accNo);
            }
            else if (specialType is AccountTypes.SpecialAccountType.BalanceStock)
            {
                DeleteBalanceStockAccount(accNo);
            }
            else
            {
                DeleteNormalAccount(accNo);
            }
            
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public void DeletePaymentMethod(string accNo)
    {
        using var transaction = ctx.Resource.Database.BeginTransaction();
        try
        {
            ctx.Resource.PaymentMethods.Where(row => row.BankAccount == accNo).ExecuteDelete();
            ctx.Resource.Glmasts.Where(row => row.AccNo == accNo).ExecuteDelete();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void DeleteFixedAssetAccount(string accNo)
    {
        using var transaction = ctx.Resource.Database.BeginTransaction();
        try
        {
            ctx.Resource.AssetLinks.Where(row => row.AssetAccNo == accNo).ExecuteDelete();
            ctx.Resource.Glmasts.Where(row => row.AccNo == accNo).ExecuteDelete();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void DeleteAccumulatedDepreciationAccount(string accNo)
    {
        using var transaction = ctx.Resource.Database.BeginTransaction();
        try
        {
            ctx.Resource.AssetLinks.Where(row => row.AssetDeprnAccNo == accNo).ExecuteDelete();
            ctx.Resource.Glmasts.Where(row => row.AccNo == accNo).ExecuteDelete();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void DeleteOpenStockAccount(string accNo)
    {
        var stockSetKeys = ctx.Resource.StockSets
            .Where(stockSet => stockSet.OpenStock == accNo)
            .Select(stockSet => stockSet.StockSetKey)
            .ToList();
        using var transaction = ctx.Resource.Database.BeginTransaction();
        try
        {
            ctx.Resource.StockPbalances
                .Where(pBalance => stockSetKeys.Contains(pBalance.StockSetKey))
                .ExecuteDelete();
            ctx.Resource.StockSets
                .Where(stockSet => stockSet.OpenStock == accNo)
                .ExecuteDelete();
            ctx.Resource.Glmasts.Where(row => row.AccNo == accNo).ExecuteDelete();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void DeleteCloseStockAccount(string accNo)
    {
        var stockSetKeys = ctx.Resource.StockSets
            .Where(stockSet => stockSet.CloseStock == accNo)
            .Select(stockSet => stockSet.StockSetKey)
            .ToList();
        using var transaction = ctx.Resource.Database.BeginTransaction();
        try
        {
            ctx.Resource.StockPbalances
                .Where(pBalance => stockSetKeys.Contains(pBalance.StockSetKey))
                .ExecuteDelete();
            ctx.Resource.StockSets
                .Where(stockSet => stockSet.CloseStock == accNo)
                .ExecuteDelete();
            ctx.Resource.Glmasts.Where(row => row.AccNo == accNo).ExecuteDelete();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
    
    public void DeleteBalanceStockAccount(string accNo)
    {
        var stockSetKeys = ctx.Resource.StockSets
            .Where(stockSet => stockSet.BalanceStock == accNo)
            .Select(stockSet => stockSet.StockSetKey)
            .ToList();
        using var transaction = ctx.Resource.Database.BeginTransaction();
        try
        {
            ctx.Resource.StockPbalances
                .Where(pBalance => stockSetKeys.Contains(pBalance.StockSetKey))
                .ExecuteDelete();
            ctx.Resource.StockSets
                .Where(stockSet => stockSet.BalanceStock == accNo)
                .ExecuteDelete();
            ctx.Resource.Glmasts.Where(row => row.AccNo == accNo).ExecuteDelete();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    #endregion

    #region UPDATE

    public void RemoveSelfCircleLinks()
    {
        ctx.Resource.Glmasts
            .Where(row => row.AccNo == row.ParentAccNo)
            .ExecuteUpdate(row => row.SetProperty(col => col.ParentAccNo, (string?)null));
    }

    #endregion

    #region SELECT 

    public bool IsEarningAccountRetained()
    {
        return ctx.Resource.Glmasts.Any(gl => gl.SpecialAccType == "SRE");
    }
    
    public bool IsAccountExist(string accNo)
    {
        return ctx.Resource.Glmasts.Any(gl => gl.AccNo == accNo);
    }

    public bool IsBsTypeAccount(string accNo)
    {
        var row = ctx.Resource.AccTypes.FirstOrDefault(gl => gl.AccTypeCode == accNo);
        return row != null && Converter.TextToBoolean(row.IsBstype);
    }
    
    public int NextStockSetKey()
    {
        var maxKey = ctx.Resource.StockSets
            .Max(stockSet => (int?)stockSet.StockSetKey) ?? 0;

        return maxKey + 1;
    }

    public List<Glmast>? GetAccount(string accNo)
    {
        var dt = ctx.Resource.Glmasts.Where(row => row.AccNo == accNo).ToList();
        if (dt.Count == 0) return null;
        return dt;
    }

    #endregion
    
    #region INSERT

    public void SaveFixedAsset(DataSet ds)
    {
        var faAcc = ds.Tables["FixedAsset"];
        var accDep = ds.Tables["AccumulatedDepreciation"];
        if (faAcc == null || faAcc.Rows.Count == 0) return;
        if (accDep == null || accDep.Rows.Count == 0) return;
        
        using var transaction = ctx.Resource.Database.BeginTransaction();
        try
        {
            _saveAccount(faAcc);
            _saveAccount(accDep);

            ctx.Resource.AssetLinks.Add(new AssetLink()
            {
                AssetAccNo = faAcc.Rows[0]["AccNo"].ToString() ?? throw new MissingPropException<string>("Invalid 'Account No' provided in Fixed Asset Account'",faAcc.ToXml()),
                AssetDeprnAccNo = accDep.Rows[0]["AccNo"].ToString() ?? throw new MissingPropException<string>("Invalid 'Account No' provided in Accumulated Depreciation Account'",accDep.ToXml()),
            });
            
            ctx.Resource.SaveChanges();
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public void SaveStockAccount(DataSet ds)
    {
        var opnAcc = ds.Tables["OpenStock"];
        var clsAcc = ds.Tables["CloseStock"];
        var balAcc = ds.Tables["BalanceStock"];
        
        if (opnAcc == null || opnAcc.Rows.Count == 0) return;
        if (clsAcc == null || clsAcc.Rows.Count == 0) return;
        if (balAcc == null || balAcc.Rows.Count == 0) return;
        
        using var transaction = ctx.Resource.Database.BeginTransaction();
        try
        {
            _saveAccount(opnAcc);
            _saveAccount(clsAcc);
            _saveAccount(balAcc);
            
            ctx.Resource.StockSets.Add(new StockSet()
            {
                StockSetKey = NextStockSetKey(),
                OpenStock = opnAcc.Rows[0]["AccNo"].ToString() ?? throw new MissingPropException<string>("Invalid 'Account No' provided in Open Stock Account",opnAcc.ToXml()),
                CloseStock = clsAcc.Rows[0]["AccNo"].ToString() ?? throw new MissingPropException<string>("Invalid 'Account No' provided in Close Stock Account",opnAcc.ToXml()),
                BalanceStock = balAcc.Rows[0]["AccNo"].ToString() ?? throw new MissingPropException<string>("Invalid 'Account No' provided in Balance Stock Account",opnAcc.ToXml()),
            });
            
            ctx.Resource.SaveChanges();
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }
    
    public void SaveAccount(DataTable acc)
    {
        using var transaction = ctx.Resource.Database.BeginTransaction();
        try
        {
            _saveAccount(acc);
            ctx.Resource.SaveChanges();
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public void SaveGlMasterPaymentMethod(DataSet ds)
    {
        var glMast = ds.Tables["GlMaster"];
        var pmMethod = ds.Tables["PaymentMethods"];
        
        if (glMast == null || glMast.Rows.Count == 0) return;
        if (pmMethod == null || pmMethod.Rows.Count == 0) return;
        
        using var transaction = ctx.Resource.Database.BeginTransaction();
        try
        {
            _saveAccount(glMast);
            
            foreach (DataRow row in pmMethod.Rows)
            {
                switch (row.RowState)
                {
                    case DataRowState.Added:
                    {
                        var newMethod = row.MapToEntity<PaymentMethods>();
                        var trackedEntity = ctx.Resource.PaymentMethods.Local
                            .FirstOrDefault(x => x.PaymentMethod == newMethod.PaymentMethod);
                        
                        if (trackedEntity != null)
                        {
                            ctx.Resource.Entry(trackedEntity).CurrentValues.SetValues(newMethod);
                        }
                        else
                        {
                            ctx.Resource.PaymentMethods.Add(newMethod);
                        }
                        break;
                    }
                    case DataRowState.Modified:
                    {
                        var accType = ctx.Resource.PaymentMethods
                            .FirstOrDefault(x => x.PaymentMethod == row["PaymentMethod"].ToString());
                        accType?.UpdateFromDataRow(row);
                        break;
                    }
                    case DataRowState.Deleted:
                    {
                        ctx.Resource.PaymentMethods.Where(x => x.PaymentMethod == row["PaymentMethod"].ToString()).ExecuteDelete();
                        break;
                    }
                }
            }
            
            ctx.Resource.SaveChanges();
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    private void _saveAccount(DataTable acc)
    {
        foreach (DataRow row in acc.Rows)
        {
            switch (row.RowState)
            {
                case DataRowState.Added:
                {
                    var newGlMast = row.MapToEntity<Glmast>();
                    var trackedEntity = ctx.Resource.Glmasts.Local
                        .FirstOrDefault(x => x.AccNo == newGlMast.AccNo);
                        
                    if (trackedEntity != null)
                    {
                        ctx.Resource.Entry(trackedEntity).CurrentValues.SetValues(newGlMast);
                    }
                    else
                    {
                        ctx.Resource.Glmasts.Add(newGlMast);
                    }
                    break;
                }
                case DataRowState.Modified:
                {
                    var accType = ctx.Resource.Glmasts
                        .FirstOrDefault(x => x.AccNo == row["AccNo"].ToString());
                    accType?.UpdateFromDataRow(row);
                    break;
                }
                case DataRowState.Deleted:
                {
                    ctx.Resource.Glmasts.Where(x => x.AccNo == row["AccNo"].ToString()).ExecuteDelete();
                    break;
                }
            }
        }
    }
    #endregion
}