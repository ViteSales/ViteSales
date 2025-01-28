using Clerk.Net.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ViteSales.ERP.Auth.Models;

namespace ViteSales.ERP.Auth.Extensions;

public static class AuthOptionBuilder
{
    public static IHttpClientBuilder AddAuthApiClient(this IServiceCollection collection,
        AuthOptionBuilderProperties options)
    {
        return collection.AddClerkApiClient(opt =>
        {
            opt.SecretKey = options.SecretKey;
        });
    }
}