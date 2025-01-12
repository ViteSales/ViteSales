using ViteSales.ERP.Cloud.Const;

namespace ViteSales.ERP.Cloud.Extensions;

public static class DbRegionExtensions
{
    public static string GetName(this Regions region)
    {
        return region switch
        {
            Regions.UsEastNVirginia => "US East (N. Virginia)",
            Regions.UsEastOhio => "US East (Ohio)",
            Regions.UsWestOregon => "US West (Oregon)",
            Regions.EuropeLondon => "Europe",
            Regions.AsiaPacificSingapore => "Asia Pacific (Singapore)",
            Regions.AsiaPacificSydney => "Asia Pacific (Sydney)",
            _ => throw new ArgumentOutOfRangeException(nameof(region), region, null)
        };
    }

    public static string GetDbSlug(this Regions region)
    {
        return region switch
        {
            Regions.UsEastNVirginia => "aws-us-east-1",
            Regions.UsEastOhio => "aws-us-east-2",
            Regions.UsWestOregon => "aws-us-west-2",
            Regions.EuropeLondon => "aws-eu-central-1",
            Regions.AsiaPacificSingapore => "aws-ap-southeast-1",
            Regions.AsiaPacificSydney => "aws-ap-southeast-2",
            _ => throw new ArgumentOutOfRangeException(nameof(region), region, null)
        };
    }
    
    public static string GetBucketSlug(this Regions region)
    {
        return region switch
        {
            Regions.UsEastNVirginia => "us-east4",
            Regions.UsEastOhio => "us-east1",
            Regions.UsWestOregon => "us-west1",
            Regions.EuropeLondon => "europe-west3",
            Regions.AsiaPacificSingapore => "asia-southeast1",
            Regions.AsiaPacificSydney => "asia-southeast2",
            _ => throw new ArgumentOutOfRangeException(nameof(region), region, null)
        };
    }
}