using ViteSales.ERP.Cloud.Models.Request;
using ViteSales.ERP.Cloud.Models.Response;

namespace ViteSales.ERP.Cloud.Interfaces;

public interface IDatabaseCloudService
{
    Task<ProjectResponse?> CreateProjectAsync(CreateProjectRequest projectRequest);
    Task<ProjectResponse?> GetProjectAsync(string projectId);
    Task<ProjectResponse?> UpdateProjectAsync(string projectId, UpdateProjectRequest projectRequest);
    Task<ProjectResponse?> DeleteProjectAsync(string projectId);
}