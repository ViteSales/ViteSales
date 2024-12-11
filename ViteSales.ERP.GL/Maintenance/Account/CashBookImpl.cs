using System.Data;
using Microsoft.EntityFrameworkCore;
using ViteSales.Data.Constants;
using ViteSales.Data.Contracts;
using ViteSales.Data.Entities;
using ViteSales.Data.Exceptions;
using ViteSales.Data.Extensions;
using ViteSales.Data.Utils;
using ViteSales.ERP.GL.Exceptions;
using ViteSales.ERP.GL.Settings;

namespace ViteSales.ERP.GL.Maintenance.Account;

public class CashBookImpl(IViteSalesDataContext ctx)
{
    public List<Cb> LoadMaster(long docKey)
    {
        return ctx.Resource.Cbs.Where(row => row.DocKey == docKey).ToList();
    }

    public List<Cbdtl> LoadDetail(long docKey)
    {
        return ctx.Resource.Cbdtls
            .AsNoTracking()
            .Include(r => r.TaxEntity)
            .Include(r => r.AccNoNavigation)
            .Include(r => r.TaxCodeNavigation)
            .Where(row => row.DocKey == docKey)
            .ToList();
    }

    public DataSet LoadImportedGoodsDetail(long docKey)
    {
        var ds = new DataSet();
        var cbigDtl = ctx.Resource.Cbigdtls
            .AsNoTracking()
            .Include(r => r.TaxCodeNavigation)
            .Where(row => row.DocKey == docKey)
            .ToList();

        var apInvoices = ctx.Resource.Apinvoices
            .Where(row => row.DocKey == docKey)
            .ToList();
        if (apInvoices.Count > 0)
        {
            var invoice = apInvoices[0];
            var creditors = ctx.Resource.Creditors
                .Where(row => row.AccNo == invoice.CreditorCode)
                .ToList()
                .ToDataTable("AccNo");
            creditors.TableName = "Creditors";
            
            var appInvoicesTbl = apInvoices.ToDataTable("DocKey");
            appInvoicesTbl.TableName = "ApInvoices";
            
            ds.Tables.Add(creditors);
            ds.Tables.Add(appInvoicesTbl);
        }

        var dt = cbigDtl.ToDataTable("DtlKey");
        dt.TableName = "ImportedGoodsDetail";
        ds.Tables.Add(dt);
        
        foreach (var cbig in cbigDtl)
        {
            List<ApinvoiceDtl> apInvoiceDtl = [];
            switch (cbig.SourceType)
            {
                case "PB":
                    apInvoiceDtl = ctx.Resource.ApinvoiceDtls
                        .Where(row => row.DtlKey == cbig.SourceDtlKey)
                        .ToList();
                    break;
                case "CP" or "PI":
                    apInvoiceDtl = ctx.Resource.ApinvoiceDtls
                        .Where(row => row.SourceDtlKey == cbig.SourceDtlKey)
                        .ToList();
                    break;
            }

            if (apInvoiceDtl.Count > 0)
            {
                var invDtlDt = apInvoiceDtl.ToDataTable("DtlKey");
                invDtlDt.TableName = "ApInvoiceDtls";
                ds.Tables.Add(invDtlDt);
            }
        }

        return ds;
    }

    public List<CbpaymentDtl> LoadPayments(long docKey)
    {
        return ctx.Resource.CbpaymentDtls
            .Include(r => r.BankChargeTaxCodeNavigation)
            .Include(r => r.PaymentMethodsNavigation)
            .Where(row => row.DocKey == docKey).ToList();
    }

    public List<WithholdingTaxPaymentDtl> LoadWithholdingTaxPayments(long docKey)
    {
        return ctx.Resource.WithholdingTaxPaymentDtls
            .Include(r => r.DeptNoNavigation)
            .Include(r => r.ProjNoNavigation)
            .Include(r => r.WithholdingTaxCodeNavigation)
            .Where(row => row.DocKey == docKey)
            .Where(row => row.DocType == "OR" || row.DocType == "PV")
            .ToList();
    }

    public Cb GetDefaultMaster(CashBook.CashBookType cashBookType)
    {
        var curr = new CurrencyImpl(ctx);
        var defaultCurrency = curr.GetDefaultCurrency();
        if (defaultCurrency == null)
        {
            throw new NoCurrencyException<string>("Default currency is not set");
        }
        return new Cb
        {
            DocType = cashBookType.GetString(),
            DocNo = Properties.NextDocNo,
            Cancelled = Converter.BooleanToText(false),
            CurrencyCode = defaultCurrency.LocalCurrencyCode,
            CurrencyRate = 1m,
            ToTaxCurrencyRate = 1m,
            DocDate = DateTime.Now,
            LastUpdate = -1,
            LastModifiedUserId = "",
            LastModified = DateTime.MinValue,
            CreatedUserId = "",
            CreatedTimeStamp = DateTime.MinValue,
            InclusiveTax = Converter.BooleanToText(false),
            RoundingMethod = Properties.RoundingMethod,
            WithholdingTaxVersion = Properties.TaxWithholdingMethod
        };
    }

    public Cbdtl GetDefaultDetail(long docKey, int seq)
    {
        return new Cbdtl()
        {
            DocKey = docKey,
            Seq = seq,
            ToAccountRate = 1,
            ProjNo = ctx.DefaultProjectNo,
            DeptNo = ctx.DefaultDepartmentNo,
            InclusiveTax = Converter.BooleanToText(false),
        };
    }

    public CbpaymentDtl GetDefaultPaymentDetail(long docKey, int seq)
    {
        return new CbpaymentDtl()
        {
            DocKey = docKey,
            Seq = seq,
            PaymentMethod = "",
            IsRchq = Converter.BooleanToText(false),
            ToBankRate = 1,
            PaymentAmt = 0,
            FloatDay = 0,
            BankCharge = 0,
            BankChargeProjNo = ctx.DefaultProjectNo,
            BankChargeDeptNo = ctx.DefaultDepartmentNo,
        };
    }

    public void Save(DataSet ds)
    {
        var cb = ds.Tables["CashBook"];
        var cbDtls = ds.Tables["CashBookDetail"];
        var cbPaymentDtls = ds.Tables["CashBookPayment"];

        if (cb == null)
        {
            throw new MissingPropException<string>("Invalid Cash Book data provided in data set");
        }
        if (cbDtls == null)
        {
            throw new MissingPropException<string>("Invalid Cash Book Detail data provided in data set");
        }
        if (cbPaymentDtls == null)
        {
            throw new MissingPropException<string>("Invalid Cash Book Payment data provided in data set");
        }

        var docDate = Convert.ToDateTime(cb.Rows[0]["DocDate"]);
        
        var fy = new FiscalYearImpl(ctx);
        fy.CheckTransactionDate(docDate);

        try
        {
            var taxDate = Convert.ToDateTime(cb.Rows[0]["TaxDate"]);
            fy.CheckTransactionDate(taxDate);
        }
        catch(Exception) {}
        
    }
    
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