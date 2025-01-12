using ViteSales.ERP.Cloud.Models.Request;
using ViteSales.ERP.Cloud.Models.Response;

namespace ViteSales.ERP.Cloud.Interfaces;

public interface IDatabaseCloudService
{
    Task<ProjectResponse?> CreateProject(CreateProjectRequest projectRequest);
    Task<ProjectResponse?> GetProject(string projectId);
    Task<ProjectResponse?> UpdateProject(string projectId, UpdateProjectRequest projectRequest);
    Task<ProjectResponse?> DeleteProject(string projectId);
}