using Microsoft.Extensions.Options;
using ViteSales.ERP.Auth.Interfaces;
using ViteSales.ERP.Shared.Models;

namespace ViteSales.ERP.Auth.Services.Kinde;

public class AuthenticationService(IOptions<AuthSecrets> secrets): IAuthentication
{
    public Task<Uri?> GetAuthorizationUri()
    {
        throw new NotImplementedException();
    }

    public Uri GetLogoutUri()
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsStateValid(string state)
    {
        throw new NotImplementedException();
    }
}