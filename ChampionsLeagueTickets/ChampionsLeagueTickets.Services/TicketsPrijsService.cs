using ChampionsLeagueTickets.Repositories.Interfaces;
using ChampionsLeagueTickets.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services
{
    public class TicketsPrijsService : ITicketPrijsService
    {

        private readonly ITicketPrijsDAO _ticketPrijsDAO;

        public TicketsPrijsService(ITicketPrijsDAO ticketPrijsDAO)
        {
            _ticketPrijsDAO = ticketPrijsDAO;
        }

        public Task<decimal> GetTicketPrijsByMatchAndSectionAsync(string MatchID, string VakNummer)
        {
            return _ticketPrijsDAO.GetTicketPrijsByMatchAndSectionAsync(MatchID, VakNummer);
        }
    }
}
