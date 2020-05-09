using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Voluntariat.Models;

namespace Voluntariat.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _apiKey;
        private readonly string _fromName;
        private readonly string _fromEmail;

        public EmailSender(IConfiguration configuration)
        {
            _apiKey = configuration["SendGridApiKey"];
            _fromName = configuration["FromName"];
            _fromEmail = configuration["FromEmail"];
        }

        public AuthMessageSenderOptions Options { get; }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SendGridClient(_apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_fromEmail, _fromName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            await client.SendEmailAsync(msg);
        }
    }
}
