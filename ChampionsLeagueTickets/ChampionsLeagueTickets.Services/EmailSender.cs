using ChampionsLeagueTickets.Services.Interfaces;
using MailKit;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
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
            var s = _config.GetSection("EmailSettings");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(s["SenderName"], s["SenderEmail"]));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;
            message.Body = new BodyBuilder { HtmlBody = htmlMessage }.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(s["MailServer"], int.Parse(s["MailPort"]!), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(s["Username"], s["Password"]);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);

            _logger.LogInformation("Email sent to {Email} — {Subject}", email, subject);
        }

        public Task SendOrderConfirmationAsync(string email, string userName, string matchName, int ticketCount)
        {
            throw new NotImplementedException();
        }
    }
}
