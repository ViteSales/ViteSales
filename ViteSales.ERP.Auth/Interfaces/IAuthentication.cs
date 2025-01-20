namespace ViteSales.ERP.Auth.Interfaces;

public interface IAuthentication
{
    public Task<Uri?> GetAuthorizationUriAsync();
    public Uri GetLogoutUri();
    public Task<bool> IsStateValidAsync(string state);
}