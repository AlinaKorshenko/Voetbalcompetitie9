using ChampionsLeagueTickets.Services.Mail.Interfaces;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace ChampionsLeagueTickets.Services.Mail
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

                await smtp.AuthenticateAsync(s["Username"], password);

                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gefaald om een mail te sturen naar {Email} — {Subject}", email, subject);
                throw;
            }
        }

        public async Task SendOrderConfirmationAsync(
                string email,
                DateTime orderDate,
                List<string> tickets,
                List<string> abonnementen,
                decimal total,
                List<(byte[] File, string FileName)> attachments
            )
            {
            try
            {
                var s = _config.GetSection("EmailSettings");
                var password = _config["EmailSettingsPassword"];

                var subject = "Bevestiging van je bestelling";

                var body = EmailTemplateService.OrderConfirmationSimple(
                    orderDate,
                    tickets,
                    abonnementen,
                    total
                );

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(s["SenderName"], s["SenderEmail"]));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = subject;

                var builder = new BodyBuilder
                {
                    HtmlBody = body
                };

                foreach (var att in attachments)
                {
                    builder.Attachments.Add(att.FileName, att.File);
                }

                message.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(s["MailServer"], int.Parse(s["MailPort"]!), SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(s["Username"], password);
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gefaald om een order te sturen naar {Email}", email);
                throw;
            }
        }
    }
}