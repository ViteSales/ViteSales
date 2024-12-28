using System.Text.Json;

namespace ViteSales.ERP.SDK.Models;

[Serializable]
public class GcpAuthInfo
{
    public string Type { get; set; }
    public string ProjectId { get; set; }
    public string PrivateKeyId { get; set; }
    public string PrivateKey { get; set; }
    public string ClientEmail { get; set; }
    public string ClientId { get; set; }
    public string AuthUri { get; set; }
    public string TokenUri { get; set; }
    public string AuthProviderX509CertUrl { get; set; }
    public string ClientX509CertUrl { get; set; }
    public string UniverseDomain { get; set; }

    public static GcpAuthInfo LoadFromJson(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new ArgumentException("The provided GCP Auth JSON string cannot be null or empty.", nameof(json));
        }

        var gcpAuthInfo = JsonSerializer.Deserialize<GcpAuthInfo>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        });

        if (gcpAuthInfo != null)
        {
            return new GcpAuthInfo
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
        throw new InvalidOperationException("Failed to deserialize the GCP Auth JSON into a GcpAuthInfo object.");
    }
}