namespace ViteSales.ERP.Cloud.Models.Request;

using Newtonsoft.Json;

public class CreateProjectRequest
{
    [JsonProperty("project")]
    public CreateProject? Project { get; set; }
}

public class CreateProject
{
    [JsonProperty("settings")]
    public Settings? Settings { get; set; }

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

    public bool ShouldSerializeSettings() => Settings != null;
    public bool ShouldSerializeBranch() => Branch != null;
    public bool ShouldSerializeDefaultEndpointSettings() => DefaultEndpointSettings != null;
    public bool ShouldSerializePgVersion() => PgVersion.HasValue;
    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeRegionId() => RegionId != null;
}

public class Settings
{
    [JsonProperty("maintenance_window")]
    public MaintenanceWindow? MaintenanceWindow { get; set; }
    
    [JsonProperty("enable_logical_replication")]
    public bool? EnableLogicalReplication { get; set; }

    public bool ShouldSerializeMaintenanceWindow() => MaintenanceWindow != null;
    public bool ShouldSerializeEnableLogicalReplication() => EnableLogicalReplication.HasValue;
}

public class MaintenanceWindow
{
    [JsonProperty("weekdays")]
    public int[]? Weekdays { get; set; }

    [JsonProperty("start_time")]
    public string? StartTime { get; set; }

    [JsonProperty("end_time")]
    public string? EndTime { get; set; }

    public bool ShouldSerializeWeekdays() => Weekdays != null;
    public bool ShouldSerializeStartTime() => StartTime != null;
    public bool ShouldSerializeEndTime() => EndTime != null;
}

public class DefaultEndpointSettings
{
    [JsonProperty("autoscaling_limit_min_cu")]
    public double? AutoscalingLimitMinCu { get; set; }

    [JsonProperty("autoscaling_limit_max_cu")]
    public double? AutoscalingLimitMaxCu { get; set; }

    [JsonProperty("suspend_timeout_seconds")]
    public int? SuspendTimeoutSeconds { get; set; }
    
    [JsonProperty("provisioner")]
    public string? Provisioner { get; set; }

    public bool ShouldSerializeProvisioner() => Provisioner != null;
    public bool ShouldSerializeAutoscalingLimitMinCu() => AutoscalingLimitMinCu.HasValue;
    public bool ShouldSerializeAutoscalingLimitMaxCu() => AutoscalingLimitMaxCu.HasValue;
    public bool ShouldSerializeSuspendTimeoutSeconds() => SuspendTimeoutSeconds.HasValue;
}

public class Branch
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("role_name")]
    public string? RoleName { get; set; }

    [JsonProperty("database_name")]
    public string? DatabaseName { get; set; }

    public bool ShouldSerializeName() => Name != null;
    public bool ShouldSerializeRoleName() => RoleName != null;
    public bool ShouldSerializeDatabaseName() => DatabaseName != null;
}