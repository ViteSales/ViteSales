using Newtonsoft.Json;
namespace ViteSales.ERP.Cloud.Models.Request;

public class UpdateProjectRequest
{
    [JsonProperty("project")]
    public Project Project { get; set; }

    public bool ShouldSerializeProject() => Project != null;
}

public class Project
{
    [JsonProperty("default_endpoint_settings")]
    public DefaultEndpointSettings? DefaultEndpointSettings { get; set; }

    public bool ShouldSerializeDefaultEndpointSettings() => DefaultEndpointSettings != null;
}

public class DefaultEndpointSettings
{
    [JsonProperty("autoscaling_limit_min_cu")]
    public int? AutoscalingLimitMinCu { get; set; }

    [JsonProperty("autoscaling_limit_max_cu")]
    public int? AutoscalingLimitMaxCu { get; set; }

    [JsonProperty("suspend_timeout_seconds")]
    public int? SuspendTimeoutSeconds { get; set; }

    public bool ShouldSerializeAutoscalingLimitMinCu() => AutoscalingLimitMinCu.HasValue;
    public bool ShouldSerializeAutoscalingLimitMaxCu() => AutoscalingLimitMaxCu.HasValue;
    public bool ShouldSerializeSuspendTimeoutSeconds() => SuspendTimeoutSeconds.HasValue;
}