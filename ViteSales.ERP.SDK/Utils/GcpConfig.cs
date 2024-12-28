using Google.Apis.Auth.OAuth2;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.SDK.Utils;

public class GcpConfig
{
    public GoogleCredential Credential { get; set; }
    public GcpAuthInfo AuthInfo { get; set; }
    public static GcpConfig ReadGcpJsonFile()
    {
        const string fileName = "vitesales.json";
        var filePath = Path.Combine(AppContext.BaseDirectory, fileName);

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file '{fileName}' was not found in the current project folder.", filePath);
        }

        return new GcpConfig
        {
            Credential = GoogleCredential.FromFile(filePath),
            AuthInfo = GcpAuthInfo.LoadFromJson(File.ReadAllText(filePath))
        };
    }
}