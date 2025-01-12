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
