namespace ViteSales.ERP.Cloud.Models.Response;

using Newtonsoft.Json;

public class ProjectResponse
{
    [JsonProperty("project")]
    public Project Project { get; set; }

    [JsonProperty("connection_uris")]
    public List<ConnectionUri> ConnectionUris { get; set; }

    [JsonProperty("roles")]
    public List<Role> Roles { get; set; }

    [JsonProperty("databases")]
    public List<Database> Databases { get; set; }

    [JsonProperty("operations")]
    public List<Operation> Operations { get; set; }

    [JsonProperty("branch")]
    public Branch Branch { get; set; }

    [JsonProperty("endpoints")]
    public List<Endpoint> Endpoints { get; set; }
}

public class Project
{
    [JsonProperty("data_storage_bytes_hour")]
    public int DataStorageBytesHour { get; set; }

    [JsonProperty("data_transfer_bytes")]
    public int DataTransferBytes { get; set; }

    [JsonProperty("written_data_bytes")]
    public int WrittenDataBytes { get; set; }

    [JsonProperty("compute_time_seconds")]
    public int ComputeTimeSeconds { get; set; }

    [JsonProperty("active_time_seconds")]
    public int ActiveTimeSeconds { get; set; }

    [JsonProperty("cpu_used_sec")]
    public int CpuUsedSec { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("platform_id")]
    public string PlatformId { get; set; }

    [JsonProperty("region_id")]
    public string RegionId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("provisioner")]
    public string Provisioner { get; set; }

    [JsonProperty("default_endpoint_settings")]
    public DefaultEndpointSettings DefaultEndpointSettings { get; set; }

    [JsonProperty("settings")]
    public Settings Settings { get; set; }

    [JsonProperty("pg_version")]
    public int PgVersion { get; set; }

    [JsonProperty("proxy_host")]
    public string ProxyHost { get; set; }

    [JsonProperty("branch_logical_size_limit")]
    public int BranchLogicalSizeLimit { get; set; }

    [JsonProperty("branch_logical_size_limit_bytes")]
    public long BranchLogicalSizeLimitBytes { get; set; }

    [JsonProperty("store_passwords")]
    public bool StorePasswords { get; set; }

    [JsonProperty("creation_source")]
    public string CreationSource { get; set; }

    [JsonProperty("history_retention_seconds")]
    public int HistoryRetentionSeconds { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("consumption_period_start")]
    public DateTime ConsumptionPeriodStart { get; set; }

    [JsonProperty("consumption_period_end")]
    public DateTime ConsumptionPeriodEnd { get; set; }

    [JsonProperty("owner_id")]
    public string OwnerId { get; set; }
}

public class DefaultEndpointSettings
{
    [JsonProperty("autoscaling_limit_min_cu")]
    public double AutoscalingLimitMinCu { get; set; }

    [JsonProperty("autoscaling_limit_max_cu")]
    public double AutoscalingLimitMaxCu { get; set; }

    [JsonProperty("suspend_timeout_seconds")]
    public int SuspendTimeoutSeconds { get; set; }
}

public class Settings
{
    [JsonProperty("allowed_ips")]
    public AllowedIps AllowedIps { get; set; }

    [JsonProperty("enable_logical_replication")]
    public bool EnableLogicalReplication { get; set; }

    [JsonProperty("block_public_connections")]
    public bool BlockPublicConnections { get; set; }

    [JsonProperty("block_vpc_connections")]
    public bool BlockVpcConnections { get; set; }
}

public class AllowedIps
{
    [JsonProperty("ips")]
    public List<object> Ips { get; set; }

    [JsonProperty("protected_branches_only")]
    public bool ProtectedBranchesOnly { get; set; }
}

public class ConnectionUri
{
    [JsonProperty("connection_uri")]
    public string ConnectionUriStr { get; set; }

    [JsonProperty("connection_parameters")]
    public ConnectionParameters ConnectionParameters { get; set; }
}

public class ConnectionParameters
{
    [JsonProperty("database")]
    public string Database { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }

    [JsonProperty("role")]
    public string Role { get; set; }

    [JsonProperty("host")]
    public string Host { get; set; }

    [JsonProperty("pooler_host")]
    public string PoolerHost { get; set; }
}

public class Role
{
    [JsonProperty("branch_id")]
    public string BranchId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }

    [JsonProperty("protected")]
    public bool Protected { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }
}

public class Database
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("branch_id")]
    public string BranchId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("owner_name")]
    public string OwnerName { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }
}

public class Operation
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("project_id")]
    public string ProjectId { get; set; }

    [JsonProperty("branch_id")]
    public string BranchId { get; set; }

    [JsonProperty("endpoint_id")]
    public string EndpointId { get; set; }

    [JsonProperty("action")]
    public string Action { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("failures_count")]
    public int FailuresCount { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("total_duration_ms")]
    public int TotalDurationMs { get; set; }
}

public class Branch
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("project_id")]
    public string ProjectId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("current_state")]
    public string CurrentState { get; set; }

    [JsonProperty("pending_state")]
    public string PendingState { get; set; }

    [JsonProperty("state_changed_at")]
    public DateTime StateChangedAt { get; set; }

    [JsonProperty("creation_source")]
    public string CreationSource { get; set; }

    [JsonProperty("primary")]
    public bool Primary { get; set; }

    [JsonProperty("default")]
    public bool Default { get; set; }

    [JsonProperty("protected")]
    public bool Protected { get; set; }

    [JsonProperty("cpu_used_sec")]
    public int CpuUsedSec { get; set; }

    [JsonProperty("compute_time_seconds")]
    public int ComputeTimeSeconds { get; set; }

    [JsonProperty("active_time_seconds")]
    public int ActiveTimeSeconds { get; set; }

    [JsonProperty("written_data_bytes")]
    public int WrittenDataBytes { get; set; }

    [JsonProperty("data_transfer_bytes")]
    public int DataTransferBytes { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }
}

public class Endpoint
{
    [JsonProperty("host")]
    public string Host { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("project_id")]
    public string ProjectId { get; set; }

    [JsonProperty("branch_id")]
    public string BranchId { get; set; }

    [JsonProperty("autoscaling_limit_min_cu")]
    public double AutoscalingLimitMinCu { get; set; }

    [JsonProperty("autoscaling_limit_max_cu")]
    public double AutoscalingLimitMaxCu { get; set; }

    [JsonProperty("region_id")]
    public string RegionId { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("current_state")]
    public string CurrentState { get; set; }

    [JsonProperty("pending_state")]
    public string PendingState { get; set; }

    [JsonProperty("settings")]
    public Dictionary<string, object> Settings { get; set; }

    [JsonProperty("pooler_enabled")]
    public bool PoolerEnabled { get; set; }

    [JsonProperty("pooler_mode")]
    public string PoolerMode { get; set; }

    [JsonProperty("disabled")]
    public bool Disabled { get; set; }

    [JsonProperty("passwordless_access")]
    public bool PasswordlessAccess { get; set; }

    [JsonProperty("creation_source")]
    public string CreationSource { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("proxy_host")]
    public string ProxyHost { get; set; }

    [JsonProperty("suspend_timeout_seconds")]
    public int SuspendTimeoutSeconds { get; set; }

    [JsonProperty("provisioner")]
    public string Provisioner { get; set; }
}