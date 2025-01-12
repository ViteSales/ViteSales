using ViteSales.ERP.Cloud.Const;

namespace ViteSales.ERP.Cloud.Extensions;

public static class DbComputeExtensions
{
    public static int GetName(this DbCompute compute)
    {
        return compute switch
        {
            DbCompute.Size1 => 1,
            DbCompute.Size1To5 => 5,
            DbCompute.Size5To10 => 10,
            DbCompute.Size10To20 => 20,
            DbCompute.Size20To50 => 50,
            DbCompute.Size50To100 => 100,
            DbCompute.Size100Plus => 1000,
            _ => throw new ArgumentOutOfRangeException(nameof(compute), compute, null)
        };
    }

    public static int GetSize(this DbCompute compute)
    {
        return compute switch
        {
            DbCompute.Size1 => 1,
            DbCompute.Size1To5 => 2,
            DbCompute.Size5To10 => 3,
            DbCompute.Size10To20 => 4,
            DbCompute.Size20To50 => 5,
            DbCompute.Size50To100 => 6,
            DbCompute.Size100Plus => 9,
            _ => throw new ArgumentOutOfRangeException(nameof(compute), compute, null)
        };
    }
}