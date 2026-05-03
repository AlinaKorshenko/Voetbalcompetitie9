using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services.Pdf.Interfaces
{
    public interface IPdfService
    {
        public byte[] GenerateTicketPdf(decimal prijs, string thuisTeamNaam, string bezoekendTeamNaam, DateTime datumTijdStartMatch, string vakOmschrijving, string rijNummer, string stoelNummer);
        public byte[] GenerateAbonnementPdf(string vakOmschrijving, string rijNummer, string stoelNummer, DateOnly startDatum, DateOnly eindDatum, string seizoenOmschrijving, string stadionNaam);
    }
}
