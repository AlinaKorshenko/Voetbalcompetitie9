using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services.Interfaces
{
    public interface ITicketPrijsService
    {

        Task<decimal> GetTicketPrijsByMatchAndSectionAsync(string MatchID, string VakNummer);
    }
}
