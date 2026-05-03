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
        public async Task AddAsync(Ticket entity)
        {
            await _ticketsDAO.AddAsync(entity);
        }

        public Task DeleteAsync(Ticket entity)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket?> FindByMatchAndSeatAsync(string matchId, string seatId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GenerateNextTicketIdAsync()
        {
            return await _ticketsDAO.GenerateNextTicketIdAsync();
        }

        public async Task<int> GetAantalTicketsVoorMatchEnUser(string userId, string matchId)
        {
            return await _ticketsDAO.GetAantalTicketsVoorMatchEnUser(userId, matchId);
        }

        public async Task<IEnumerable<Ticket>?> GetAllAsync()
        {
            return await _ticketsDAO.GetAllAsync();
        }

        public async Task<bool> HasTicketOnSameDay(string userId, string matchId, DateTime matchDatum)
        {
            return await _ticketsDAO.HasTicketOnSameDay(userId, matchId, matchDatum);
        }

        public Task UpdateAsync(Ticket entity)
        {
            throw new NotImplementedException();
        }
    }
}
