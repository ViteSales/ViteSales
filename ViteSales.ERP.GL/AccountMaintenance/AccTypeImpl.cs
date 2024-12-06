using System.Data;
using Microsoft.EntityFrameworkCore;
using ViteSales.Data.Contracts;
using ViteSales.Data.Entities;
using ViteSales.Data.Extensions;
using ViteSales.Data.Utils;

namespace ViteSales.ERP.GL.AccountMaintenance;

public class AccTypeImpl(IViteSalesDataContext ctx)
{
    public DataTable Load()
    {
        return ctx.Resource.AccTypes.ToList().ToDataTable("AccTypeCode");
    }

    public void Save(DataTable dt)
    {
        using var transaction = ctx.Resource.Database.BeginTransaction();
        try
        {
            var accGroups = ctx.Resource.AccGroups.ToList().ToDataTable("AccType");
            
            var rowsToDelete = dt.Rows.Cast<DataRow>()
                .Where(row => row.RowState == DataRowState.Deleted)
                .ToList(); 
            
            var isBsTypeSeq = ctx.Resource.AccGroups
                .Join(ctx.Resource.AccTypes,
                    ag => ag.AccType,
                    at => at.AccTypeCode,
                    (ag, at) => new { AccGroup = ag, AccType = at })
                .Where(x => x.AccType.IsBstype == "T")
                .Max(x => (int?)x.AccGroup.Seq) ?? 0;
            
            var isNotBsTypeSeq = ctx.Resource.AccGroups
                .Join(ctx.Resource.AccTypes,
                    ag => ag.AccType,
                    at => at.AccTypeCode,
                    (ag, at) => new { AccGroup = ag, AccType = at })
                .Where(x => x.AccType.IsBstype == "F")
                .Max(x => (int?)x.AccGroup.Seq) ?? 0;

            isBsTypeSeq++;
            isNotBsTypeSeq++;
            
            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    var isBsType = 
                        Converter.TextToBoolean(row.Field<string>("IsBstype")  == null ? 
                            "F" : row.Field<string>("IsBstype"));

                    var currentAccType = row.Field<string>("AccTypeCode");
                    if (currentAccType != null)
                    {
                        var groupHasAccType = accGroups.Rows.Find(currentAccType);
                        if (groupHasAccType == null)
                        {
                            var nextGroupRow = accGroups.NewRow();
                            if (isBsType)
                            {
                                nextGroupRow["Seq"] = isBsTypeSeq++;
                                nextGroupRow["Group"] = "AS";
                            }
                            if (!isBsType)
                            {
                                nextGroupRow["Seq"] = isNotBsTypeSeq++;
                                nextGroupRow["Group"] = "NP";
                            }

                            nextGroupRow["Level"] = 1;
                            nextGroupRow["NormalBalance"] = "C";
                            nextGroupRow["AccType"] = currentAccType;
                            accGroups.Rows.Add(nextGroupRow);
                        
                            ctx.Resource.AccGroups.Add(nextGroupRow.MapToEntity<AccGroup>());
                        }
                    }
                }
                switch (row.RowState)
                {
                    case DataRowState.Added:
                        var newAccType = row.MapToEntity<AccType>();
                        var trackedEntity = ctx.Resource.AccTypes.Local
                            .FirstOrDefault(x => x.AccTypeCode == newAccType.AccTypeCode);
                        
                        if (trackedEntity != null)
                        {
                            ctx.Resource.Entry(trackedEntity).CurrentValues.SetValues(newAccType);
                        }
                        else
                        {
                            ctx.Resource.AccTypes.Add(newAccType);
                        }
                        break;

                    case DataRowState.Modified:
                        var accType = ctx.Resource.AccTypes
                            .FirstOrDefault(x => x.AccTypeCode == row["AccTypeCode"].ToString());
                        if (accType != null)
                        {
                            accType.UpdateFromDataRow(row);
                        }
                        break;
                }
            }

            foreach (var accTypeToDelete in rowsToDelete.Select(row => row["AccTypeCode", DataRowVersion.Original].ToString()).Select(accTypeKey => ctx.Resource.AccTypes
                         .FirstOrDefault(x => x.AccTypeCode == accTypeKey)).OfType<AccType>())
            {
                ctx.Resource.AccTypes.Remove(accTypeToDelete);
                ctx.Resource.AccGroups.Where(t => t.AccType == accTypeToDelete.AccTypeCode).ToList().ForEach(t => ctx.Resource.AccGroups.Remove(t));
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
}