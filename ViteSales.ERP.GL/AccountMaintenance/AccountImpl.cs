using System.Data;
using Microsoft.EntityFrameworkCore;
using ViteSales.Data.Constants;
using ViteSales.Data.Contracts;
using ViteSales.Data.Entities;
using ViteSales.Data.Exceptions;
using ViteSales.Data.Extensions;
using ViteSales.Data.Models;
using ViteSales.Data.Utils;
using ViteSales.ERP.GL.Exceptions;

namespace ViteSales.ERP.GL.AccountMaintenance;

public class AccountImpl
{
    private readonly IViteSalesDataContext _ctx;
    public AccountImpl(IViteSalesDataContext ctx)
    {
        _ctx = ctx;
        RemoveSelfCircleLinks();
    }
    #region DELETE

    public void DeleteNormalAccount(string accNo)
    {
        _ctx.Resource.Glmasts.Where(row => row.AccNo == accNo).ExecuteDelete();
    }

    public void DeleteAccount(string accNo)
    {
        var fiscalYearImpl = new FiscalYearImpl(_ctx);
        var firstRow = _ctx.Resource.Glmasts.FirstOrDefault(row => row.AccNo == accNo);
        if (firstRow == null)
        {
            throw new MissingPropException<string>("Invalid 'Account No' provided",accNo);
        }
        var specialType = AccountTypes.ToSpecialAccountType(firstRow.SpecialAccType);
        var (startPeriod, _) = fiscalYearImpl.GetFiscalYearPeriod();
        using var transaction = _ctx.Resource.Database.BeginTransaction();
        try
        {
            var fiscalStartDate = startPeriod.StartDate.ToString("yyyy-MM-dd");
            _ctx.Resource.Obalances
                .Where(row => row.AccNo == accNo)
                .ExecuteDelete();
            _ctx.Resource.Pbalances
                .Where(row => row.AccNo == accNo)
                .ExecuteDelete();
            _ctx.Resource.Database
                .ExecuteSqlRaw($"DELETE FROM \"TaxTransAuditDTL\" WHERE \"TaxTransKey\" NOT IN (SELECT \"TaxTransKey\" FROM \"TaxTrans\") AND \"TaxDate\" < '{fiscalStartDate}' AND \"Action\" != 2 AND (\"TaxableAccNo\" = '{accNo}' OR \"TaxAccNo\" = '{accNo}');");
            _ctx.Resource.Database
                .ExecuteSqlRaw($"DELETE FROM \"TaxTransAuditDTL\" AS \"A\" WHERE \"A\".\"TaxTransKey\" NOT IN (SELECT \"TaxTransKey\" FROM \"TaxTrans\") AND \"A\".\"TaxDate\" < '{fiscalStartDate}' AND \"A\".\"Action\" = 3 AND EXISTS(SELECT 1 FROM \"TaxTransAuditDTL\" AS \"B\" WHERE \"B\".\"Action\" = 2 AND \"A\".\"TaxTransAuditKey\" = \"B\".\"TaxTransAuditKey\" AND \"A\".\"TaxTransKey\" = \"B\".\"TaxTransKey\" AND (\"B\".\"TaxableAccNo\" = '{accNo}' OR \"B\".\"TaxAccNo\" = '{accNo}'))");
            _ctx.Resource.Database
                .ExecuteSqlRaw(
                    "DELETE FROM \"TaxTransAuditDTL\" AS \"A\" WHERE \"A\".\"Action\" = 2 AND NOT EXISTS (SELECT 1 FROM \"TaxTransAuditDTL\" AS \"B\" WHERE \"B\".\"Action\" = 3 AND \"A\".\"TaxTransAuditKey\" = \"B\".\"TaxTransAuditKey\" AND \"A\".\"TaxTransKey\" = \"B\".\"TaxTransKey\")");
            _ctx.Resource.Database
                .ExecuteSqlRaw(
                    "DELETE FROM \"TaxTransAudit\" WHERE \"TaxTransAuditKey\" NOT IN (SELECT \"TaxTransAuditKey\" FROM \"TaxTransAuditDTL\")");
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
        catch (Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }
    }

    public void DeletePaymentMethod(string accNo)
    {
        using var transaction = _ctx.Resource.Database.BeginTransaction();
        try
        {
            _ctx.Resource.PaymentMethods.Where(row => row.BankAccount == accNo).ExecuteDelete();
            _ctx.Resource.Glmasts.Where(row => row.AccNo == accNo).ExecuteDelete();
            transaction.Commit();
        }
        catch(Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }
    }

    public void DeleteFixedAssetAccount(string accNo)
    {
        using var transaction = _ctx.Resource.Database.BeginTransaction();
        try
        {
            _ctx.Resource.AssetLinks.Where(row => row.AssetAccNo == accNo).ExecuteDelete();
            _ctx.Resource.Glmasts.Where(row => row.AccNo == accNo).ExecuteDelete();
            transaction.Commit();
        }
        catch(Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }
    }

    public void DeleteAccumulatedDepreciationAccount(string accNo)
    {
        using var transaction = _ctx.Resource.Database.BeginTransaction();
        try
        {
            _ctx.Resource.AssetLinks.Where(row => row.AssetDeprnAccNo == accNo).ExecuteDelete();
            _ctx.Resource.Glmasts.Where(row => row.AccNo == accNo).ExecuteDelete();
            transaction.Commit();
        }
        catch(Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }
    }

    public void DeleteOpenStockAccount(string accNo)
    {
        var stockSetKeys = _ctx.Resource.StockSets
            .Where(stockSet => stockSet.OpenStock == accNo)
            .Select(stockSet => stockSet.StockSetKey)
            .ToList();
        using var transaction = _ctx.Resource.Database.BeginTransaction();
        try
        {
            _ctx.Resource.StockPbalances
                .Where(pBalance => stockSetKeys.Contains(pBalance.StockSetKey))
                .ExecuteDelete();
            _ctx.Resource.StockSets
                .Where(stockSet => stockSet.OpenStock == accNo)
                .ExecuteDelete();
            _ctx.Resource.Glmasts.Where(row => row.AccNo == accNo).ExecuteDelete();
            transaction.Commit();
        }
        catch(Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }
    }

    public void DeleteCloseStockAccount(string accNo)
    {
        var stockSetKeys = _ctx.Resource.StockSets
            .Where(stockSet => stockSet.CloseStock == accNo)
            .Select(stockSet => stockSet.StockSetKey)
            .ToList();
        using var transaction = _ctx.Resource.Database.BeginTransaction();
        try
        {
            _ctx.Resource.StockPbalances
                .Where(pBalance => stockSetKeys.Contains(pBalance.StockSetKey))
                .ExecuteDelete();
            _ctx.Resource.StockSets
                .Where(stockSet => stockSet.CloseStock == accNo)
                .ExecuteDelete();
            _ctx.Resource.Glmasts.Where(row => row.AccNo == accNo).ExecuteDelete();
            transaction.Commit();
        }
        catch(Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }
    }
    
    public void DeleteBalanceStockAccount(string accNo)
    {
        var stockSetKeys = _ctx.Resource.StockSets
            .Where(stockSet => stockSet.BalanceStock == accNo)
            .Select(stockSet => stockSet.StockSetKey)
            .ToList();
        using var transaction = _ctx.Resource.Database.BeginTransaction();
        try
        {
            _ctx.Resource.StockPbalances
                .Where(pBalance => stockSetKeys.Contains(pBalance.StockSetKey))
                .ExecuteDelete();
            _ctx.Resource.StockSets
                .Where(stockSet => stockSet.BalanceStock == accNo)
                .ExecuteDelete();
            _ctx.Resource.Glmasts.Where(row => row.AccNo == accNo).ExecuteDelete();
            transaction.Commit();
        }
        catch(Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }
    }

    #endregion

    #region UPDATE

    public void RemoveSelfCircleLinks()
    {
        _ctx.Resource.Glmasts
            .Where(row => row.AccNo == row.ParentAccNo)
            .ExecuteUpdate(row => row.SetProperty(col => col.ParentAccNo, (string?)null));
    }

    #endregion

    #region SELECT

    public Glmast GetDefaultGlMast()
    {
        var generalSettings = new SettingsImpl(_ctx).Get<SettingsGeneral>("General");
        if (generalSettings == null)
        {
            throw new NoCurrencyException<string>("No Currency found in General Settings");
        }
        return new Glmast()
        {
            AccNo = "",
            AccType = "",
            CurrencyCode = generalSettings.LocalCurrencyCode,
            CashFlowCategory = CashFlow.FromCashFlowCategory(CashFlow.CashFlowCategory.OperatingActivities),
            Guid = Guid.NewGuid(),
        };
    }
    
    public bool IsEarningAccountRetained()
    {
        return _ctx.Resource.Glmasts.Any(gl => gl.SpecialAccType == "SRE");
    }
    
    public bool IsAccountExist(string accNo)
    {
        return _ctx.Resource.Glmasts.Any(gl => gl.AccNo == accNo);
    }

    public bool IsBsTypeAccount(string accNo)
    {
        var row = _ctx.Resource.AccTypes.FirstOrDefault(gl => gl.AccTypeCode == accNo);
        return row != null && Converter.TextToBoolean(row.IsBstype);
    }
    
    public int NextStockSetKey()
    {
        var maxKey = _ctx.Resource.StockSets
            .Max(stockSet => (int?)stockSet.StockSetKey) ?? 0;

        return maxKey + 1;
    }

    public List<Glmast>? GetAccount(string accNo)
    {
        var dt = _ctx.Resource.Glmasts.Where(row => row.AccNo == accNo).ToList();
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
        
        using var transaction = _ctx.Resource.Database.BeginTransaction();
        try
        {
            _saveAccount(faAcc);
            _saveAccount(accDep);

            _ctx.Resource.AssetLinks.Add(new AssetLink()
            {
                AssetAccNo = faAcc.Rows[0]["AccNo"].ToString() ?? throw new MissingPropException<string>("Invalid 'Account No' provided in Fixed Asset Account'",faAcc.ToXml()),
                AssetDeprnAccNo = accDep.Rows[0]["AccNo"].ToString() ?? throw new MissingPropException<string>("Invalid 'Account No' provided in Accumulated Depreciation Account'",accDep.ToXml()),
            });
            
            _ctx.Resource.SaveChanges();
            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw ex;
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
        
        using var transaction = _ctx.Resource.Database.BeginTransaction();
        try
        {
            _saveAccount(opnAcc);
            _saveAccount(clsAcc);
            _saveAccount(balAcc);
            
            _ctx.Resource.StockSets.Add(new StockSet()
            {
                StockSetKey = NextStockSetKey(),
                OpenStock = opnAcc.Rows[0]["AccNo"].ToString() ?? throw new MissingPropException<string>("Invalid 'Account No' provided in Open Stock Account",opnAcc.ToXml()),
                CloseStock = clsAcc.Rows[0]["AccNo"].ToString() ?? throw new MissingPropException<string>("Invalid 'Account No' provided in Close Stock Account",opnAcc.ToXml()),
                BalanceStock = balAcc.Rows[0]["AccNo"].ToString() ?? throw new MissingPropException<string>("Invalid 'Account No' provided in Balance Stock Account",opnAcc.ToXml()),
            });
            
            _ctx.Resource.SaveChanges();
            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }
    }
    
    public void SaveNormalAccount(DataTable acc)
    {
        using var transaction = _ctx.Resource.Database.BeginTransaction();
        try
        {
            _saveAccount(acc);
            _ctx.Resource.SaveChanges();
            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }
    }

    public void SaveGlMasterPaymentMethod(DataSet ds)
    {
        var glMast = ds.Tables["GlMaster"];
        var pmMethod = ds.Tables["PaymentMethods"];
        
        if (glMast == null || glMast.Rows.Count == 0) return;
        if (pmMethod == null || pmMethod.Rows.Count == 0) return;
        
        using var transaction = _ctx.Resource.Database.BeginTransaction();
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
                        var trackedEntity = _ctx.Resource.PaymentMethods.Local
                            .FirstOrDefault(x => x.PaymentMethod == newMethod.PaymentMethod);
                        
                        if (trackedEntity != null)
                        {
                            _ctx.Resource.Entry(trackedEntity).CurrentValues.SetValues(newMethod);
                        }
                        else
                        {
                            _ctx.Resource.PaymentMethods.Add(newMethod);
                        }
                        break;
                    }
                    case DataRowState.Modified:
                    {
                        var accType = _ctx.Resource.PaymentMethods
                            .FirstOrDefault(x => x.PaymentMethod == row["PaymentMethod"].ToString());
                        accType?.UpdateFromDataRow(row);
                        break;
                    }
                    case DataRowState.Deleted:
                    {
                        _ctx.Resource.PaymentMethods.Where(x => x.PaymentMethod == row["PaymentMethod"].ToString()).ExecuteDelete();
                        break;
                    }
                }
            }
            
            _ctx.Resource.SaveChanges();
            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw ex;
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
                    var trackedEntity = _ctx.Resource.Glmasts.Local
                        .FirstOrDefault(x => x.AccNo == newGlMast.AccNo);
                        
                    if (trackedEntity != null)
                    {
                        _ctx.Resource.Entry(trackedEntity).CurrentValues.SetValues(newGlMast);
                    }
                    else
                    {
                        _ctx.Resource.Glmasts.Add(newGlMast);
                    }
                    break;
                }
                case DataRowState.Modified:
                {
                    var accType = _ctx.Resource.Glmasts
                        .FirstOrDefault(x => x.AccNo == row["AccNo"].ToString());
                    accType?.UpdateFromDataRow(row);
                    break;
                }
                case DataRowState.Deleted:
                {
                    _ctx.Resource.Glmasts.Where(x => x.AccNo == row["AccNo"].ToString()).ExecuteDelete();
                    break;
                }
            }
        }
    }
    #endregion
}