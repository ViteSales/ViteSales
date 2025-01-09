using DigitalOcean.API;
using DigitalOcean.API.Models.Requests;

namespace ViteSales.ERP.Deployment;

public class ViteSalesDeployment(string apiKey)
{
    public async Task Connect()
    {
        var client = new DigitalOceanClient(apiKey);
    }
}