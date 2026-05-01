using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services.Mail.Interfaces
{
    public interface IAppEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);

        Task SendOrderConfirmationAsync(string email,
            DateTime orderDate,
            List<string> tickets,
            List<string> abonnementen,
            decimal total,
            List<(byte[] File, string FileName)> attachments);
    }
}
