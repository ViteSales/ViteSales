using System.Text.Json;
using Google.Apis.Auth.OAuth2;
using ViteSales.ERP.Shared.Models;

namespace ViteSales.ERP.Shared.Utils;

public class AppSettings
{
    public LoggingConfig Logging { get; set; } = null!;
    public DefaultDbCredentials DefaultDb { get; set; } = null!;
    public CacheDbCredentials CacheDb { get; set; } = null!;
    public AuthSecrets AuthSecrets { get; set; } = null!;
    public GcpCredentials GcpCredentials { get; set; } = null!;
    public GoogleCredential GoogleCredential { get; set; } = null!;
    public ServerCredential ServerCredential { get; set; } = null!;
    public object GcpServerCredential { get; set; } = null!;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public GcpCredentials GetGcpCredential()
    {
        ArgumentNullException.ThrowIfNull(GcpServerCredential);
        return GcpCredentials.LoadFromJson(JsonSerializer.Serialize(GcpServerCredential));
    }

    public GoogleCredential GetGoogleCredential()
    {
        ArgumentNullException.ThrowIfNull(GcpServerCredential);
        return GoogleCredential.FromJson(JsonSerializer.Serialize(GcpServerCredential));
    }
    
    public static AppSettings Read()
    {
        var appSettingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        if (!File.Exists(appSettingsPath))
            throw new FileNotFoundException("appsettings.json not found");
        return Read(File.ReadAllText(appSettingsPath));
    }

    public static AppSettings Read(string json)
    {
        return JsonSerializer.Deserialize<AppSettings>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException("Failed to deserialize appsettings.json");
    }
}