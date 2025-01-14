using ViteSales.ERP.Shared.Models;

namespace ViteSales.ERP.SDK.Interfaces;

public interface ISendEmailService
{
    public Task SendEmailAsync(EmailData emailData);
}