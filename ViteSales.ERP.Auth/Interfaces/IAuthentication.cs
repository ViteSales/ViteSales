namespace ViteSales.ERP.Auth.Interfaces;

public interface IAuthentication
{
    public Task<Uri?> GetAuthorizationUri();
    public Uri GetLogoutUri();
    public Task<bool> IsStateValid(string state);
}