using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MS2.Services
{
    public class SendGridEmailSender : IEmailSender
    {
        public const string BURNER_ADMIN_EMAIL = "jennatolls006@gmail.com";

        public SendGridEmailSenderOptions Options { get; set; }

        public SendGridEmailSender(IOptions<SendGridEmailSenderOptions> opt)
        {
            Options = opt.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await Execute(Options.ApiKey, subject, message, email);
        }

        private async Task<Response> Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Options.SenderEmail, Options.SenderName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };

            msg.AddTo(new EmailAddress(email));
            msg.AddTo(new EmailAddress(BURNER_ADMIN_EMAIL));

            return await client.SendEmailAsync(msg);
        }
    }
}
