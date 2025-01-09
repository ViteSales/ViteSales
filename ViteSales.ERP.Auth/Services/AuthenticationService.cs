using Auth0.AuthenticationApi.Builders;
using Auth0.AuthenticationApi.Models;
using Microsoft.Extensions.Options;
using ViteSales.ERP.Auth.Interfaces;
using ViteSales.ERP.Shared.Interfaces;
using ViteSales.ERP.Shared.Models;

namespace ViteSales.ERP.Auth.Services;

public class AuthenticationService(IOptions<AuthSecrets> secrets, ICacheClient cache): IAuthentication
{
    public async Task<Uri?> GetAuthorizationUri()
    {
        ArgumentNullException.ThrowIfNull(secrets.Value);
        ArgumentNullException.ThrowIfNull(cache);
        
        var key = Guid.NewGuid().ToString();
        if (await cache.Set(key, "whatever", TimeSpan.FromMinutes(5)))
        {
            return new AuthorizationUrlBuilder(new Uri(secrets.Value.AuthDomain))
                .WithAudience(secrets.Value.Audience)
                .WithState(key)
                .WithClient(secrets.Value.ClientId)
                .WithResponseType(AuthorizationResponseType.Token)
                .WithAudience(secrets.Value.Audience)
                .Build();
        }
        return null;
    }

    public async Task<bool> IsStateValid(string state)
    {
        var value = await cache.Get<string>(state);
        return !string.IsNullOrEmpty(value);
    }
}