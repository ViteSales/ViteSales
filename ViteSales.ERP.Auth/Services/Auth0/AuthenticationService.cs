using Auth0.AuthenticationApi.Builders;
using Auth0.AuthenticationApi.Models;
using Auth0.ManagementApi.Clients;
using Microsoft.Extensions.Options;
using ViteSales.ERP.Auth.Interfaces;
using ViteSales.ERP.Shared.Interfaces;
using ViteSales.ERP.Shared.Models;

namespace ViteSales.ERP.Auth.Services.Auth0;

public class AuthenticationService(IOptions<AuthSecrets> secrets, ICacheClient cache): IAuthentication
{
    public async Task<Uri?> GetAuthorizationUriAsync()
    {
        ArgumentNullException.ThrowIfNull(secrets.Value);
        ArgumentNullException.ThrowIfNull(cache);
        
        var key = Guid.NewGuid().ToString();
        if (await cache.SetAsync(key, "whatever", TimeSpan.FromMinutes(5)))
        {
            return new AuthorizationUrlBuilder(new Uri(secrets.Value.AuthDomain))
                .WithAudience(secrets.Value.Audience)
                .WithState(key)
                .WithClient(secrets.Value.ClientId)
                .WithResponseType(AuthorizationResponseType.Token)
                .WithAudience(secrets.Value.Audience)
                .WithRedirectUrl(secrets.Value.RedirectUri)
                .Build();
        }
        throw new InvalidOperationException("Could not set cache value");
    }

    public Uri GetLogoutUri()
    {
        return new LogoutUrlBuilder(new Uri(secrets.Value.AuthDomain))
            .WithClientId(secrets.Value.ClientId)
            .Build();
    }

    public async Task<bool> IsStateValidAsync(string state)
    {
        var value = await cache.GetAsync<string>(state);
        return !string.IsNullOrEmpty(value);
    }
}