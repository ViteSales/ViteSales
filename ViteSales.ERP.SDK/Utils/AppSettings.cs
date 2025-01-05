using System.Text.Json;
using Google.Apis.Auth.OAuth2;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.SDK.Utils;

public class AppSettings
{
    public LoggingConfig Logging { get; set; } = null!;
    public AuthCredentialConfig AuthCredential { get; set; } = null!;
    public GcpCredentials GcpCredentials { get; set; } = null!;
    public GoogleCredential GoogleCredential { get; set; } = null!;
    public object ServerCredential { get; set; } = null!;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public GcpCredentials GetGcpCredential()
    {
        ArgumentNullException.ThrowIfNull(ServerCredential);
        return GcpCredentials.LoadFromJson(JsonSerializer.Serialize(ServerCredential));
    }

    public GoogleCredential GetGoogleCredential()
    {
        ArgumentNullException.ThrowIfNull(ServerCredential);
        return GoogleCredential.FromJson(JsonSerializer.Serialize(ServerCredential));
    }
    
    public static AppSettings Read()
    {
        var appSettingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        if (!File.Exists(appSettingsPath))
            throw new FileNotFoundException("appsettings.json not found");
        var json = File.ReadAllText(appSettingsPath);
        return JsonSerializer.Deserialize<AppSettings>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException("Failed to deserialize appsettings.json");
    }
}