namespace ViteSales.ERP.Cloud.Models.Request;

using Newtonsoft.Json;

public class CreateProjectRequest
{
    [JsonProperty("project")]
    public CreateProject? Project { get; set; }
}

public class CreateProject
{
    [JsonProperty("branch")]
    public Branch? Branch { get; set; }

    [JsonProperty("default_endpoint_settings")]
    public DefaultEndpointSettings? DefaultEndpointSettings { get; set; }

    [JsonProperty("pg_version")]
    public int? PgVersion { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("region_id")]
    public string? RegionId { get; set; }

    [JsonProperty("store_passwords")]
    public bool? StorePasswords { get; set; }
    
    public bool ShouldSerializeStorePasswords() => StorePasswords.HasValue;
    public bool ShouldSerializePgVersion() => PgVersion.HasValue;
    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeRegionId() => RegionId != null;
    public bool ShouldSerializeBranch() => Branch != null;
    public bool ShouldSerializeDefaultEndpointSettings() => DefaultEndpointSettings != null;
}

public class Branch
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("role_name")]
    public string? RoleName { get; set; }

    [JsonProperty("database_name")]
    public string? DatabaseName { get; set; }
}

