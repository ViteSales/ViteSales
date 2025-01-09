using System.Text.Json;

namespace ViteSales.ERP.Shared.Models;

[Serializable]
public class LoggingConfig
{
    public LogLevelsConfig LogLevel { get; set; } = null!;
}
[Serializable]
public class LogLevelsConfig
{
    public string Default { get; set; } = null!;
    public string System { get; set; } = null!;
    public string Microsoft { get; set; } = null!;
}
[Serializable]
public class DefaultDbCredentials
{
    public required string Host { get; set; }
    public required int Port { get; set; }
    public required string User { get; set; }
    public required string Password { get; set; }
    public required string Database { get; set; }
}
[Serializable]
public class AuthSecrets
{
    public required string AuthorityUrl { get; set; }
    public required string AuthDomain { get; set; }
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
    public required string Audience { get; set; }
    public required string GrantType { get; set; }
}
[Serializable]
public class GcpCredentials
{
    public string Type { get; set; } = null!;
    public string ProjectId { get; set; } = null!;
    public string PrivateKeyId { get; set; } = null!;
    public string PrivateKey { get; set; } = null!;
    public string ClientEmail { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string AuthUri { get; set; } = null!;
    public string TokenUri { get; set; } = null!;
    public string AuthProviderX509CertUrl { get; set; } = null!;
    public string ClientX509CertUrl { get; set; } = null!;
    public string UniverseDomain { get; set; } = null!;

    public static GcpCredentials LoadFromJson(string json)
    {
        if (string.IsNullOrEmpty(json))
        {
            throw new ArgumentException("The provided GCP Auth JSON string cannot be null or empty.", nameof(json));
        }

        var gcpAuthInfo = JsonSerializer.Deserialize<GcpCredentials>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        });

        if (gcpAuthInfo != null)
        {
            return new GcpCredentials
            {
                Type = gcpAuthInfo.Type,
                ProjectId = gcpAuthInfo.ProjectId,
                PrivateKeyId = gcpAuthInfo.PrivateKeyId,
                PrivateKey = gcpAuthInfo.PrivateKey,
                ClientEmail = gcpAuthInfo.ClientEmail,
                ClientId = gcpAuthInfo.ClientId,
                AuthUri = gcpAuthInfo.AuthUri,
                TokenUri = gcpAuthInfo.TokenUri,
                AuthProviderX509CertUrl = gcpAuthInfo.AuthProviderX509CertUrl,
                ClientX509CertUrl = gcpAuthInfo.ClientX509CertUrl,
                UniverseDomain = gcpAuthInfo.UniverseDomain
            };
        }
        throw new InvalidOperationException("Failed to deserialize the GCP Auth JSON into a GcpCredentials object.");
    }
}