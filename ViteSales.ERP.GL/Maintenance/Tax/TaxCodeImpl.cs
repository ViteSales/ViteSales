using System.Data;
using Microsoft.EntityFrameworkCore;
using ViteSales.Data.Contracts;
using ViteSales.Data.Entities;
using ViteSales.Data.Extensions;
using ViteSales.Data.Utils;
using ViteSales.ERP.GL.Exceptions;

namespace ViteSales.ERP.GL.Maintenance.Tax;

public class TaxCodeImpl(IViteSalesDataContext ctx)
{
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
                if (row.RowState is DataRowState.Added or DataRowState.Modified)
                {
                    var taxCodeType  = row.Field<string>("SupplyPurchase");
                    if (taxCodeType == null)
                    {
                        throw new TaxCodeException<string>("Tax type is empty");
                    }
                    if (taxCodeType != "S" && taxCodeType != "P")
                    {
                        throw new TaxCodeException<string>("Tax type is invalid");
                    }
                    var isDefault = Converter.TextToBoolean(row.Field<string>("IsDefault"));
                    if (isDefault)
                    {
                        ctx.Resource.TaxCodes.Where(x => x.IsDefault == "T" && x.SupplyPurchase == taxCodeType)
                            .ExecuteUpdate(r =>
                                r.SetProperty(x => x.IsDefault, "F"));
                    }
                }
            }

            foreach (DataRow row in dt.Rows)
            {
                switch (row.RowState)
                {
                    case DataRowState.Added:
                        var newTaxCode = row.MapToEntity<TaxCodes>();
                        var trackedEntity = ctx.Resource.TaxCodes.Local
                            .FirstOrDefault(x => x.TaxCode == newTaxCode.TaxCode);
                        
                        if (trackedEntity != null)
                        {
                            ctx.Resource.Entry(trackedEntity).CurrentValues.SetValues(newTaxCode);
                        }
                        else
                        {
                            ctx.Resource.TaxCodes.Add(newTaxCode);
                        }
                        break;
                    case DataRowState.Modified:
                        var taxCodes = ctx.Resource.TaxCodes
                            .FirstOrDefault(x => x.TaxCode == row["TaxCode"].ToString());
                        taxCodes?.UpdateFromDataRow(row);
                        break;
                }
            }
            
            foreach (var taxCodeToDelete in rowsToDelete.Select(row => row["TaxCode", DataRowVersion.Original].ToString()).Select(taxCode => ctx.Resource.TaxCodes
                         .FirstOrDefault(x => x.TaxCode == taxCode)).OfType<TaxCodes>())
            {
                ctx.Resource.TaxCodes.Remove(taxCodeToDelete);
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