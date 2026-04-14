using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories.Interfaces;
using ChampionsLeagueTickets.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services
{
    public class TicketService : ITicketService
    {

        private ITicketDAO _ticketsDAO;

        public TicketService(ITicketDAO ticketsDAO)
        {
            _ticketsDAO = ticketsDAO;
        }
        public Task AddAsync(Ticket entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Ticket entity)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Ticket>?> GetAllAsync()
        {
            return await _ticketsDAO.GetAllAsync();
        }

        public Task UpdateAsync(Ticket entity)
        {
            throw new NotImplementedException();
        }
    }
}
