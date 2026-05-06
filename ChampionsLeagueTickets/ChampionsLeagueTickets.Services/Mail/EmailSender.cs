using Azure.Core;
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
        private readonly IConfiguration _configuration;
        private readonly string _mailServer;
        private readonly int _mailPort;
        private readonly string _senderName;
        private readonly string _senderEmail;
        private readonly string _username;
        private readonly string _password;

        public EmailSender(IConfiguration config)
        {
            _configuration = config;

            _mailServer = _configuration["EmailSettings:MailServer"]!;
            _mailPort = int.Parse(_configuration["EmailSettings:MailPort"]!);
            _senderName = _configuration["EmailSettings:SenderName"]!;
            _senderEmail = _configuration["EmailSettings:SenderEmail"]!;
            _username = _configuration["EmailSettings:Username"]!;
            _password = _configuration["EmailSettingsPassword"]!;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_senderName, _senderEmail));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = subject;
                message.Body = new BodyBuilder { HtmlBody = htmlMessage }.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_mailServer, _mailPort, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_username, _password);
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Gefaald om een mail te sturen naar '{email}', met onderwerp '{subject}' en bericht '{htmlMessage}': ", ex);
            }
        }

        public async Task SendOrderConfirmationAsync(
            string email,
            DateTime orderDate,
            List<string> tickets,
            List<string> abonnementen,
            decimal total,
            List<(byte[] File, string FileName)> attachments)
        {
            try
            {
                var subject = "Bevestiging van je bestelling";
                var body = EmailTemplateService.OrderConfirmationSimple(orderDate, tickets, abonnementen, total);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_senderName, _senderEmail));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = subject;

                var builder = new BodyBuilder { HtmlBody = body };
                foreach (var att in attachments)
                    builder.Attachments.Add(att.FileName, att.File);

                message.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_mailServer, _mailPort, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_username, _password);
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Gefaald om een orderconfirmation mail te sturen naar '{email}': ", ex);
            }
        }
    }
}