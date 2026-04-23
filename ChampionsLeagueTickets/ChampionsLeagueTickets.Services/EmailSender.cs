using ChampionsLeagueTickets.Services.Interfaces;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace ChampionsLeagueTickets.Services
{
    public class EmailSender : IAppEmailSender, IEmailSender
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IConfiguration config, ILogger<EmailSender> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var s = _config.GetSection("EmailSettings");

                var password = _config["EmailSettingsPassword"];

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(s["SenderName"], s["SenderEmail"]));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = subject;
                message.Body = new BodyBuilder { HtmlBody = htmlMessage }.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(s["MailServer"], int.Parse(s["MailPort"]!), SecureSocketOptions.StartTls);

                // 🔐 hier mix je appsettings + Key Vault
                await smtp.AuthenticateAsync(s["Username"], password);

                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email} — {Subject}", email, subject);
                throw;
            }
        }

        public Task SendOrderConfirmationAsync(string email, string userName, string matchName, int ticketCount)
        {
            throw new NotImplementedException();
        }
    }
}