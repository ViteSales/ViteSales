using RestSharp;
using Newtonsoft.Json;
using ViteSales.ERP.Auth.Interfaces;
using ViteSales.ERP.Shared.Models;

namespace ViteSales.ERP.Auth.Client;

public class AccessToken(AuthSecrets authSecrets): IAccessToken
{
    public string Get()
    {
        var client = new RestClient(authSecrets.AuthorityUrl);
        var req = new RestRequest
        {
            Method = Method.Post
        };
        req.AddHeader("content-type", "application/json");
        req.AddParameter("application/json", JsonConvert.SerializeObject(new Dictionary<string, string>
        {
            {"client_id",authSecrets.ClientId},
            {"client_secret",authSecrets.ClientSecret},
            {"audience",authSecrets.Audience},
            {"grant_type",authSecrets.GrantType}
        }), ParameterType.RequestBody);
        var resp = client.Execute(req);
        if ((int)resp.StatusCode < 200 || (int)resp.StatusCode >= 300)
        {
            throw new InvalidOperationException($"Authentication startup failed with status code: {resp.StatusCode}, response: {resp.Content}");
        }

        if (string.IsNullOrEmpty(resp.Content))
        {
            throw new InvalidOperationException($"Authentication startup failed invalid response content: {resp.Content}");
        }
        var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(resp.Content);
        if (jsonResponse == null || 
            !jsonResponse.ContainsKey("access_token") || 
            !jsonResponse.ContainsKey("token_type"))
        {
            throw new InvalidOperationException($"Authentication startup failed unable to get access token from response: {resp.Content}");
        }

        return $"{jsonResponse["token_type"]} {jsonResponse["access_token"]}";
    }
}