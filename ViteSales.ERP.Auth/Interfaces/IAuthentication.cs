namespace ViteSales.ERP.Auth.Interfaces;

public interface IAuthentication
{
    public Task<Uri?> GetAuthorizationUri();
    public Task<bool> IsStateValid(string state);
}