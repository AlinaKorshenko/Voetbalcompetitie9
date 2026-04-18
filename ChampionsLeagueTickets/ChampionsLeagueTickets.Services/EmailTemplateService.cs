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
        body { margin: 0; padding: 0; background-color: #0a0a0a; font-family: 'Segoe UI', Arial, sans-serif; }
        .wrapper { max-width: 600px; margin: 0 auto; background-color: #111; }
        .header { background: linear-gradient(135deg, #001f5b 0%, #0a0a0a 60%); padding: 40px 30px; text-align: center; border-bottom: 3px solid #c8a84b; }
        .header h1 { color: #c8a84b; font-size: 28px; margin: 0; letter-spacing: 3px; text-transform: uppercase; }
        .header p { color: #888; margin: 8px 0 0; font-size: 13px; letter-spacing: 2px; text-transform: uppercase; }
        .body { padding: 40px 30px; color: #ccc; line-height: 1.7; }
        .body h2 { color: #fff; font-size: 22px; margin-top: 0; }
        .btn { display: inline-block; margin: 24px 0; padding: 14px 32px; background: linear-gradient(135deg, #c8a84b, #e8c96b); color: #000 !important; font-weight: 700; text-decoration: none; border-radius: 3px; letter-spacing: 1px; text-transform: uppercase; font-size: 14px; }
        .info-box { background: #1a1a2e; border-left: 4px solid #c8a84b; padding: 16px 20px; margin: 20px 0; border-radius: 0 4px 4px 0; }
        .info-box p { margin: 4px 0; color: #aaa; font-size: 14px; }
        .info-box strong { color: #c8a84b; }
        .footer { background: #0a0a0a; padding: 24px 30px; text-align: center; border-top: 1px solid #222; }
        .footer p { color: #444; font-size: 12px; margin: 4px 0; }
        .divider { border: none; border-top: 1px solid #222; margin: 24px 0; }
        """;

        public static string ConfirmEmail(string confirmUrl) => $"""
        <!DOCTYPE html>
        <html>
        <head><meta charset="utf-8"><style>{BaseStyle}</style></head>
        <body>
          <div class="wrapper">
            <div class="header">
              <h1>⚽ UCL Tickets</h1>
              <p>UEFA Champions League</p>
            </div>
            <div class="body">
              <h2>Confirm your email address</h2>
              <p>Welcome to UCL Tickets! You're one step away from accessing the best seats in European football.</p>
              <p>Please confirm your email address to activate your account:</p>
              <a href="{confirmUrl}" class="btn">Confirm my email</a>
              <hr class="divider" />
              <p style="font-size:13px; color:#555;">If you didn't create an account, you can safely ignore this email.</p>
            </div>
            <div class="footer">
              <p>© UCL Tickets — Built for school purposes</p>
            </div>
          </div>
        </body>
        </html>
        """;

        public static string ForgotPassword(string resetUrl) => $"""
        <!DOCTYPE html>
        <html>
        <head><meta charset="utf-8"><style>{BaseStyle}</style></head>
        <body>
          <div class="wrapper">
            <div class="header">
              <h1>⚽ UCL Tickets</h1>
              <p>UEFA Champions League</p>
            </div>
            <div class="body">
              <h2>Reset your password</h2>
              <p>We received a request to reset the password for your UCL Tickets account.</p>
              <p>Click the button below to choose a new password:</p>
              <a href="{resetUrl}" class="btn">Reset my password</a>
              <hr class="divider" />
              <p style="font-size:13px; color:#555;">This link expires in <strong style="color:#c8a84b">24 hours</strong>. If you didn't request a password reset, you can safely ignore this email.</p>
            </div>
            <div class="footer">
              <p>© UCL Tickets — Built for school purposes</p>
            </div>
          </div>
        </body>
        </html>
        """;

        public static string OrderConfirmation(string userName, string matchName, int ticketCount) => $"""
        <!DOCTYPE html>
        <html>
        <head><meta charset="utf-8"><style>{BaseStyle}</style></head>
        <body>
          <div class="wrapper">
            <div class="header">
              <h1>⚽ UCL Tickets</h1>
              <p>UEFA Champions League</p>
            </div>
            <div class="body">
              <h2>Your tickets are confirmed! 🎉</h2>
              <p>Hi {userName}, your order has been placed successfully. See you at the match!</p>
              <div class="info-box">
                <p><strong>Match:</strong> {matchName}</p>
                <p><strong>Tickets:</strong> {ticketCount}x</p>
              </div>
              <p>Your tickets will be available in your account dashboard. Bring your confirmation to the stadium.</p>
            </div>
            <div class="footer">
              <p>© UCL Tickets — Built for school purposes</p>
            </div>
          </div>
        </body>
        </html>
        """;
    }
}
