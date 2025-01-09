namespace ViteSales.ERP.Deployment.Options.Datacenter;

public enum Database
{
    Nyc1,
    Nyc2,
    Nyc3,
    Sfo1,
    Sfo2,
    Sfo3,
    Tor1,
    Lon1,
    Ams2,
    Ams3,
    Fra1,
    Sgp1,
    Blr1,
    Syd1
}

public static class DatabaseCenterExtensions
{
    public static string ToRegion(this Database database)
    {
        switch (database)
        {
            case Database.Nyc1:
            case Database.Nyc2:
            case Database.Nyc3:
            case Database.Sfo1:
            case Database.Sfo2:
            case Database.Sfo3:
            case Database.Tor1:
                return "North America";
            case Database.Lon1:
            case Database.Ams2:
            case Database.Ams3:
            case Database.Fra1:
                return "Europe";
            case Database.Sgp1:
            case Database.Blr1:
                return "Asia";
            case Database.Syd1:
                return "Australia";
            default:
                throw new ArgumentOutOfRangeException(nameof(database), database, null);
        }
    }
    
    public static string ToName(this Database database)
    {
        switch (database)
        {
            case Database.Nyc1:
            case Database.Nyc2:
            case Database.Nyc3:
                return "New York";
            case Database.Sfo1:
            case Database.Sfo2:
            case Database.Sfo3:
                return "San Francisco";
            case Database.Tor1:
                return "Toronto";
            case Database.Lon1:
                return "London";
            case Database.Ams2:
            case Database.Ams3:
                return "Amsterdam";
            case Database.Fra1:
                return "Frankfurt";
            case Database.Sgp1:
                return "Singapore";
            case Database.Blr1:
                return "Bangalore";
            case Database.Syd1:
                return "Australia";
            default:
                throw new ArgumentOutOfRangeException(nameof(database), database, null);
        }
    }
    
    public static string ToTag(this Database database)
    {
        switch (database)
        {
            case Database.Nyc1:
                return "NYC1";
            case Database.Nyc2:
                return "NYC2";
            case Database.Nyc3:
                return "NYC3";
            case Database.Sfo1:
                return "SFO1";
            case Database.Sfo2:
                return "SFO2";
            case Database.Sfo3:
                return "SFO3";
            case Database.Tor1:
                return "TOR1";
            case Database.Lon1:
                return "LON1";
            case Database.Ams2:
                return "AMS2";
            case Database.Ams3:
                return "AMS3";
            case Database.Fra1:
                return "FRA1";
            case Database.Sgp1:
                return "SGP1";
            case Database.Blr1:
                return "BLR1";
            case Database.Syd1:
                return "SYD1";
            default:
                throw new ArgumentOutOfRangeException(nameof(database), database, null);
        }
    }
}