using ChampionsLeagueTickets.Domain.EntitiesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Repositories.Interfaces
{
    public interface ITicketPrijsDAO : IDAO<TicketsPrijs>
    {
        Task<decimal> GetTicketPrijsByMatchAndSectionAsync(string MatchID, string VakNummer);
    }
}
