using System.Data;
using ViteSales.Data.Contracts;
using ViteSales.Data.Entities;
using ViteSales.Data.Extensions;

namespace ViteSales.ERP.GL.Maintenance.Tax;

public class TaxTransactionsImpl(IViteSalesDataContext ctx)
{
    public bool IsTaxDateInSameVatPeriod(DateTime oldTaxDate, DateTime newTaxDate)
    {
        long oldVatKey = -1L;
        long newVatKey = -1L;

        var oldVatKeyProcessor = ctx.Resource
            .Gstprocessors
            .FirstOrDefault(row => row.FromDate <= oldTaxDate && row.ToDate >= oldTaxDate);
        var newVatKeyProcessor = ctx.Resource
            .Gstprocessors
            .FirstOrDefault(row => row.FromDate <= newTaxDate && row.ToDate >= newTaxDate);

        if (oldVatKeyProcessor != null)
        {
            oldVatKey = oldVatKeyProcessor.Gstkey;
        }
        if (newVatKeyProcessor != null)
        {
            newVatKey = newVatKeyProcessor.Gstkey;
        }
        return oldVatKey == newVatKey;
    }

    public bool IsTaxDateInSameDstPeriod(DateTime oldTaxDate, DateTime newTaxDate)
    {
        long oldVatKey = -1L;
        long newVatKey = -1L;

        var oldVatKeyProcessor = ctx.Resource
            .Sstprocessors
            .FirstOrDefault(row => row.FromDate <= oldTaxDate && row.ToDate >= oldTaxDate);
        var newVatKeyProcessor = ctx.Resource
            .Sstprocessors
            .FirstOrDefault(row => row.FromDate <= newTaxDate && row.ToDate >= newTaxDate);

        if (oldVatKeyProcessor != null)
        {
            oldVatKey = oldVatKeyProcessor.Sstkey;
        }
        if (newVatKeyProcessor != null)
        {
            newVatKey = newVatKeyProcessor.Sstkey;
        }
        return oldVatKey == newVatKey;
    }

    public string? GetGovtTaxCode(string taxCode)
    {
        var taxInfo = ctx.Resource.TaxCodes
            .FirstOrDefault(row => row.TaxCode == taxCode);
        return taxInfo?.GovtTaxCode;
    }
    
    public void Save(TaxTrans taxTrans, TaxTransAudit? audit, List<TaxTransAuditDtl>? auditDtl)
    {
        using var transaction = ctx.Resource.Database.BeginTransaction();
        try
        {
            ctx.Resource.TaxTrans.Add(taxTrans);
            if (audit != null)
            {
                var lastRecord = ctx.Resource.TaxTransAudits
                    .OrderByDescending(row => row.TaxTransAuditKey)
                    .FirstOrDefault();
                if (lastRecord != null)
                {
                    audit.TaxTransAuditKey = lastRecord.TaxTransAuditKey + 1;
                }
                else
                {
                    audit.TaxTransAuditKey = 1;
                }
                ctx.Resource.TaxTransAudits.Add(audit);
                if (auditDtl != null)
                {
                    foreach (var dtl in auditDtl)
                    {
                        dtl.TaxTransAuditKey = audit.TaxTransAuditKey;
                        ctx.Resource.TaxTransAuditDtls.Add(dtl);
                    }
                }
            }
            ctx.Resource.SaveChanges();
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw e;
        }
    }
}