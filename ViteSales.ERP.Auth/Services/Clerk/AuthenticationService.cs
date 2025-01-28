using Microsoft.Extensions.Options;
using ViteSales.ERP.Auth.Interfaces;
using ViteSales.ERP.Shared.Interfaces;
using ViteSales.ERP.Shared.Models;

namespace ViteSales.ERP.Auth.Services.Clerk;

public class AuthenticationService(IOptions<AuthSecrets> secrets, ICacheClient cache): IAuthentication
{
    public Task<Uri?> GetAuthorizationUriAsync()
    {
        throw new NotImplementedException();
    }

    public Uri GetLogoutUri()
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsStateValidAsync(string state)
    {
        throw new NotImplementedException();
    }
}