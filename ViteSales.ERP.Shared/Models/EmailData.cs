namespace ViteSales.ERP.Shared.Models;

public class EmailData
{
    public string? FromName { get; set; }
    public string FromAddress { get; set; }
    public string ToAddress { get; set; }
    public string[] CcAddress { get; set; }
    public string[] BccAddress { get; set; }
    public string Subject { get; set; }
    public string? Body { get; set; }
    public string? AttachmentPath { get; set; }
    public MemoryStream? AttachmentStream { get; private set; }

    public class Builder
    {
        private readonly EmailData _emailData = new();

        public Builder SetFromName(string name)
        {
            _emailData.FromName = name;
            return this;
        }
        
        public Builder SetFromAddress(string fromAddress)
        {
            _emailData.FromAddress = fromAddress;
            return this;
        }

        public Builder SetToAddress(string toAddress)
        {
            _emailData.ToAddress = toAddress;
            return this;
        }

        public Builder SetCcAddress(string[] ccAddress)
        {
            _emailData.CcAddress = ccAddress;
            return this;
        }

        public Builder SetBccAddress(string[] bccAddress)
        {
            _emailData.BccAddress = bccAddress;
            return this;
        }

        public Builder SetSubject(string subject)
        {
            _emailData.Subject = subject;
            return this;
        }

        public Builder SetBody(string body)
        {
            _emailData.Body = body;
            return this;
        }

        public Builder SetAttachmentPath(string attachmentPath)
        {
            _emailData.AttachmentPath = attachmentPath;
            if (Uri.IsWellFormedUriString(attachmentPath, UriKind.Absolute))
            {
                using var client = new HttpClient();
                var response = client.GetAsync(attachmentPath).Result;
                if (response.IsSuccessStatusCode)
                {
                    _emailData.AttachmentStream = new MemoryStream(response.Content.ReadAsByteArrayAsync().Result);
                }
            }
            return this;
        }

        public EmailData Build()
        {
            return _emailData;
        }
    }
}