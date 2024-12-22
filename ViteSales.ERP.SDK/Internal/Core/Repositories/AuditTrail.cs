using SqlKata.Execution;
using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Database.Operation;
using ViteSales.ERP.SDK.Internal.Core.Entities;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.SDK.Internal.Core.Repositories;

public class AuditTrail(ConnectionConfig config): DbContext(config)
{
    public async Task Insert(AuditTrailInternal audit)
    {
        audit.Id = Guid.NewGuid();
        audit.ActionAt = DateTime.Now;
        await SaveChanges(() =>
        [
            new Insert<AuditTrailInternal>(audit)
        ]);
    }

    public async Task<List<AuditTrailInternal>> Get(string moduleId, string dataId)
    {
        var factory = await Get<AuditTrailInternal>();
        var res = factory.Select("*")
            .Where("Module", moduleId)
            .Where("DataId", dataId)
            .Get<AuditTrailInternal>();
        return res == null ? [] : res.ToList();
    }
}