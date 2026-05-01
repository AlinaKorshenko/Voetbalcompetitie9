using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services.Interfaces
{
    public interface IPdfService
    {
        public byte[] GenerateTicketPdf(decimal prijs, string thuisTeamNaam, string bezoekendTeamNaam, DateTime datumTijdStartMatch, string vakOmschrijving, string rijNummer, string stoelNummer);
    }
}
