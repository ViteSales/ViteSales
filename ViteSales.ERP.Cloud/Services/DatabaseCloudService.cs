using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using ViteSales.ERP.Cloud.Interfaces;
using ViteSales.ERP.Cloud.Models.Request;
using ViteSales.ERP.Cloud.Models.Response;
using ViteSales.ERP.Shared.Utils;

namespace ViteSales.ERP.Cloud.Services;

public class DatabaseCloudService: IDatabaseCloudService
{
    private readonly RestClient _client = new(new RestClientOptions($"https://console.neon.tech/api/v2/"));
    private readonly AppSettings _appSettings;
    private readonly ILogger<DatabaseCloudService> _logger;
    public DatabaseCloudService(IOptions<AppSettings> appSettings, ILogger<DatabaseCloudService> logger)
    {
        ArgumentNullException.ThrowIfNull(appSettings.Value);
        _appSettings = appSettings.Value;
        _logger = logger;
    }
    public async Task<ProjectResponse?> CreateProject(CreateProjectRequest projectRequest)
    {
        _logger.LogInformation("Starting CreateProject request.");

        var request = new RestRequest("projects");
        request.AddHeader("accept", "application/json");
        request.AddHeader("authorization", $"Bearer {_appSettings.ServerCredential.NeonTech}");
        request.AddJsonBody(JsonConvert.SerializeObject(projectRequest));

        var response = await _client.PostAsync(request);
        
        if (response is { StatusCode: >= HttpStatusCode.OK and < (HttpStatusCode)300, Content: not null })
        {
            _logger.LogInformation("CreateProject request succeeded.");
            return JsonConvert.DeserializeObject<ProjectResponse>(response.Content);
        }

        _logger.LogError("CreateProject request failed with status code: {StatusCode}, Content: {Content}", response?.StatusCode, response?.Content);
        throw new HttpRequestException("NeonTech request failed while creating project.");
    }

    public async Task<ProjectResponse?> GetProject(string projectId)
    {
        _logger.LogInformation("Starting GetProject request for project ID: {ProjectId}.", projectId);

        var request = new RestRequest($"projects/{projectId}");
        request.AddHeader("accept", "application/json");
        request.AddHeader("authorization", $"Bearer {_appSettings.ServerCredential.NeonTech}");

        var response = await _client.GetAsync(request);

        if (response is { StatusCode: >= HttpStatusCode.OK and < (HttpStatusCode)300, Content: not null })
        {
            _logger.LogInformation("GetProject request succeeded for project ID: {ProjectId}.", projectId);
            return JsonConvert.DeserializeObject<ProjectResponse>(response.Content);
        }

        _logger.LogError("GetProject request failed for project ID: {ProjectId} with status code: {StatusCode}, Content: {Content}", projectId, response?.StatusCode, response?.Content);
        throw new HttpRequestException("NeonTech request failed while getting project.");
    }

    public async Task<ProjectResponse?> UpdateProject(string projectId, UpdateProjectRequest projectRequest)
    {
        _logger.LogInformation("Starting UpdateProject request for project ID: {ProjectId}.", projectId);

        var request = new RestRequest($"projects/{projectId}");
        request.AddHeader("accept", "application/json");
        request.AddHeader("authorization", $"Bearer {_appSettings.ServerCredential.NeonTech}");
        request.AddJsonBody(JsonConvert.SerializeObject(projectRequest));

        var response = await _client.PatchAsync(request);

        if (response is { StatusCode: >= HttpStatusCode.OK and < (HttpStatusCode)300, Content: not null })
        {
            _logger.LogInformation("UpdateProject request succeeded for project ID: {ProjectId}.", projectId);
            return JsonConvert.DeserializeObject<ProjectResponse>(response.Content);
        }

        _logger.LogError("UpdateProject request failed for project ID: {ProjectId} with status code: {StatusCode}, Content: {Content}", projectId, response?.StatusCode, response?.Content);
        throw new HttpRequestException("NeonTech request failed while updating project.");
    }

    public async Task<ProjectResponse?> DeleteProject(string projectId)
    {
        _logger.LogInformation("Starting DeleteProject request for project ID: {ProjectId}.", projectId);

        var request = new RestRequest($"projects/{projectId}");
        request.AddHeader("accept", "application/json");
        request.AddHeader("authorization", $"Bearer {_appSettings.ServerCredential.NeonTech}");

        var response = await _client.DeleteAsync(request);

        if (response is { StatusCode: >= HttpStatusCode.OK and < (HttpStatusCode)300, Content: not null })
        {
            _logger.LogInformation("DeleteProject request succeeded for project ID: {ProjectId}.", projectId);
            return JsonConvert.DeserializeObject<ProjectResponse>(response.Content);
        }

        _logger.LogError("DeleteProject request failed for project ID: {ProjectId} with status code: {StatusCode}, Content: {Content}", projectId, response?.StatusCode, response?.Content);
        throw new HttpRequestException("NeonTech request failed while deleting project.");
    }
}

