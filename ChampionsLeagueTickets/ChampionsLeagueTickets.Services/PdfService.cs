using ChampionsLeagueTickets.Services.Interfaces;
using iTextSharp.text;
using iTextSharp.text.pdf;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services
{
    public class PdfService : IPdfService
    {
        public byte[] GenerateTicketPdf(decimal prijs, string thuisTeamNaam, string bezoekendTeamNaam, DateTime datumTijdStartMatch, string vakOmschrijving, string rijNummer, string stoelNummer)
        {
            using var ms = new MemoryStream();

            var document = new Document(PageSize.A6, 20, 20, 20, 20); // klein ticket formaat
            var writer = PdfWriter.GetInstance(document, ms);

            document.Open();

            // Fonts
            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var smallFont = FontFactory.GetFont(FontFactory.HELVETICA, 8);

            // Titel
            document.Add(new Paragraph("Champions League Ticket", titleFont));
            document.Add(new Paragraph(" "));

            // Match info
            document.Add(new Paragraph($"{thuisTeamNaam} vs {bezoekendTeamNaam}", normalFont));
            document.Add(new Paragraph(datumTijdStartMatch.ToString("dd/MM/yyyy HH:mm"), normalFont));
            document.Add(new Paragraph(" "));

            // Zitplaats
            document.Add(new Paragraph($"Vak: {vakOmschrijving}", normalFont));
            document.Add(new Paragraph($"Rij: {rijNummer}", normalFont));
            document.Add(new Paragraph($"Stoel: {stoelNummer}", normalFont));
            document.Add(new Paragraph(" "));

            // Prijs
            document.Add(new Paragraph($"Prijs: €{prijs:0.00}", normalFont));
            document.Add(new Paragraph(" "));

            // 🔳 QR-code genereren
            var qrContent = $"{thuisTeamNaam}-{bezoekendTeamNaam}-{datumTijdStartMatch}-{vakOmschrijving}-{rijNummer}-{stoelNummer}";

            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new PngByteQRCode(qrData);
                var qrBytes = qrCode.GetGraphic(20);

                var qrImage = Image.GetInstance(qrBytes);
                qrImage.ScaleToFit(120f, 120f);
                qrImage.Alignment = Element.ALIGN_CENTER;

                document.Add(qrImage);
            }

            document.Add(new Paragraph(" "));
            document.Add(new Paragraph("Scan deze QR-code bij toegang.", smallFont));

            document.Close();

            return ms.ToArray();
        }
    }
}
