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

        public Task AddAsync(ZitplaatsenService entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ZitplaatsenService entity)
        {
            throw new NotImplementedException();
        }

        public Task<ZitplaatsenService?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ZitplaatsenService>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetTicketPrijsByMatchAndSectionAsync(string MatchID, string VakNummer)
        {
            return _ticketPrijsDAO.GetTicketPrijsByMatchAndSectionAsync(MatchID, VakNummer);
        }

        public Task UpdateAsync(ZitplaatsenService entity)
        {
            throw new NotImplementedException();
        }
    }
}
