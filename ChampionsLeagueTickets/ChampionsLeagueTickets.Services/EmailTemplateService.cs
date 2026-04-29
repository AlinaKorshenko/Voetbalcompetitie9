using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services
{
    public static class EmailTemplateService
    {
        private const string BaseStyle = """
        .wrapper {
            max-width: 600px; margin: 0 auto; background-color: #191d56; 
        }
        .header {
            background-color: white; padding: 40px 30px; text-align: center; border-bottom: 3px solid #c8a84b; 
        }
        .header h1 {
            color: #c8a84b; font-size: 28px; margin: 0; letter-spacing: 3px; text-transform: uppercase; 
        }
        .header p {
            color: black; margin: 8px 0 0; font-size: 13px; letter-spacing: 2px; text-transform: uppercase; 
        }
        .body {
            padding: 40px 30px; color: #ccc; line-height: 1.7; 
        }
        .body h2 {
            color: #fff; font-size: 22px; margin-top: 0; 
        }
        .btn { 
            display: inline-block; margin: 24px 0; padding: 14px 32px; background: linear-gradient(135deg, #c8a84b, #e8c96b); color: white !important; font-weight: 700; text-decoration: none; border-radius: 3px; letter-spacing: 1px; text-transform: uppercase; font-size: 14px; background-color: #c9aa71;
        }
        .info-box { 
            background: #1a1a2e; border-left: 4px solid #c8a84b; padding: 16px 20px; margin: 20px 0; border-radius: 0 4px 4px 0; 
        }
        .info-box p { 
            margin: 4px 0; color: #aaa; font-size: 14px; 
        }
        .info-box strong {
            color: #c8a84b; 
        }
        .footer { 
            background-color: white; color: black; padding: 24px 30px; text-align: center; border-top: 1px solid #222; 
        }
        .footer p { 
            font-size: 12px;
        }
        .divider { 
            border: none; border-top: 2px solid #c9aa71; margin: 24px 0;
        }
        """;

        public static string ConfirmEmail(string confirmUrl) => $"""
        <!DOCTYPE html>
        <html>
        <head><meta charset="utf-8"><style>{BaseStyle}</style></head>
        <body style="margin:0; padding:0; background-color: #0d0e1a;">
          <div class="wrapper">
            <div class="header">
              <h1>⚽ Champions League Tickets</h1>
              <p>Champions League Tickets Application E-Mailbevestiging</p>
            </div>
            <div class="body">
              <h2>Bevestig uw E-mailadres</h2>
              <p>Welkom op onze Champions League Tickets applicatie! U bent maar één stap weg van het beste Europese voetbal.</p>
              <p>Bevestig alstublieft uw E-mailadres om uw account te activeren:</p>
              <a href="{confirmUrl}" class="btn">Bevestig mijn E-mail</a>
              <hr class="divider" />
              <p style="font-size:13px;">Als u geen account heeft gemaakt, kunt u deze E-mail negeren.</p>
            </div>
            <div class="footer">
              <p>&copy; Champions League Tickets Application - schoolapplicatie gebouwd door studenten van Vives</p>
            </div>
          </div>
        </body>
        </html>
        """;

        public static string ForgotPassword(string resetUrl) => $"""
        <!DOCTYPE html>
        <html>
        <head><meta charset="utf-8"><style>{BaseStyle}</style></head>
        <body style="margin:0; padding:0; background-color: #0d0e1a;">
          <div class="wrapper">
            <div class="header">
              <h1>⚽ Champions League Tickets</h1>
              <p>Champions League Tickets Application Wachtwoord Reset</p>
            </div>
            <div class="body">
              <h2>Reset je wachtwoord</h2>
              <p>We hebben een aanvraag ontvangen om het wachtwoord van uw account te resetten.</p>
              <p>Klik op de onderstaande knop om een nieuw wachtwoord te kiezen:</p>
              <a href="{resetUrl}" class="btn">Reset mijn wachtwoord</a>
              <hr class="divider" />
              <p style="font-size:13px;">Deze link vervalt in <strong style="color:#c8a84b">24 uur</strong>. Als u geen wachtwoord-reset heeft aangevraagd, kunt u deze E-mail negeren.</p>
            </div>
            <div class="footer">
              <p>&copy; 2026 Voetbaltickets – Studentenproject Toegepaste Informatica</p>
            </div>
          </div>
        </body>
        </html>
        """;

        public static string OrderConfirmationSimple(
            string orderId,
            DateTime orderDate,
            List<string> ticketLines,
            List<string> abonnementLines,
            decimal total
        )
                {
                    var ticketsHtml = ticketLines.Any()
                        ? string.Join("", ticketLines.Select(t => $"<li>{t}</li>"))
                        : "<li>Geen tickets</li>";

                    var abonnementHtml = abonnementLines.Any()
                        ? string.Join("", abonnementLines.Select(a => $"<li>{a}</li>"))
                        : "<li>Geen abonnementen</li>";

                    return $"""
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset="utf-8">
                <style>{BaseStyle}</style>
            </head>

            <body style="margin:0; background-color:#0d0e1a;">
              <div class="wrapper">

                <div class="header">
                  <h1>⚽ Bestelbevestiging</h1>
                  <p>Order #{orderId}</p>
                </div>

                <div class="body">

                  <h2>Bedankt voor je bestelling!</h2>

                  <div class="info-box">
                    <p><strong>Order datum:</strong> {orderDate:dd/MM/yyyy HH:mm}</p>
                  </div>

                  <h3 style="color:#fff;">🎟 Tickets</h3>
                  <ul>{ticketsHtml}</ul>

                  <h3 style="color:#fff;">🏟 Abonnementen</h3>
                  <ul>{abonnementHtml}</ul>

                  <hr class="divider" />

                  <p><strong>Totaal:</strong> €{total:0.00}</p>

                  <p style="font-size:13px;">
                    Dit is een bevestiging van je bestelling. Bewaar deze e-mail goed.
                  </p>

                </div>

                <div class="footer">
                  <p>&copy; Champions League Tickets</p>
                </div>

              </div>
            </body>
            </html>
            """;
        }
    }
}
