using FluentEmail.Core;
using FluentEmail.Core.Models;
using ViteSales.ERP.SDK.Interfaces;
using EmailData = ViteSales.ERP.Shared.Models.EmailData;

namespace ViteSales.ERP.SDK.Services;

public class SendEmailService(IFluentEmail email): ISendEmailService
{
    public async Task SendEmailAsync(EmailData emailData)
    {
        await email
            .SetFrom(emailData.FromAddress, emailData.FromName)
            .To(emailData.ToAddress)
            .Subject(emailData.Subject)
            .CC(emailData.CcAddress.Select(item =>new Address(item)))
            .BCC(emailData.BccAddress.Select(item =>new Address(item)))
            .Body(emailData.Body)
            .SendAsync();
    }
}