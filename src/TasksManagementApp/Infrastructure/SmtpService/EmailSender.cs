using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace TasksManagementApp.Infrastructure.EmailService
{
    public class EmailSender
    {
        private readonly SmtpConfiguration _smtpConfiguration;

        public EmailSender(IOptions<SmtpConfiguration> smtpConfiguration)
        {
            _smtpConfiguration = smtpConfiguration.Value;
        }

        public void Send(string to, string subject, string html, string from = null)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? _smtpConfiguration.EmailFrom));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            using var smtp = new SmtpClient();
            smtp.Connect(_smtpConfiguration.SmtpHost, _smtpConfiguration.SmtpPort, true);
            smtp.AuthenticationMechanisms.Remove("XOAUTH2");
            smtp.Authenticate(_smtpConfiguration.SmtpUser, _smtpConfiguration.SmtpPassword);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
