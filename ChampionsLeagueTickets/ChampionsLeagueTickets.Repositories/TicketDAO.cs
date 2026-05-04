using ChampionsLeagueTickets.Domain.DataDB;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Repositories
{
    public class TicketDAO : ITicketDAO
    {

        private readonly FootballDbContext _dbContext;

        public TicketDAO(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Ticket entity)
        {
            try
            {
                await _dbContext.Tickets.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(Ticket entity)
        {
            try
            {
                _dbContext.Tickets.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public Task<Ticket?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GenerateNextTicketIdAsync()
        {
            try
            {
                var lastTicket = await _dbContext.Tickets
                    .OrderByDescending(o => o.TicketId)
                    .FirstOrDefaultAsync();

                if (lastTicket == null)
                    return "T0001";

                var lastNumber = int.Parse(lastTicket.TicketId.Substring(1));
                var newNumber = lastNumber + 1;

                return $"T{newNumber.ToString("D4")}";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public async Task<int> GetAantalTicketsVoorMatchEnUser(string userId, string matchId)
        {
            try
            {
                return await _dbContext.Orderlijnens
                    .Where(ol =>
                        ol.MatchId == matchId &&
                        ol.TicketId != null &&
                        ol.Order.UserId == userId)
                    .CountAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Ticket>?> GetAllAsync()
        {
            try
            {
                return await _dbContext.Tickets
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> HasTicketOnSameDay(string userId, string matchId, DateTime matchDatum)
        {
            try
            {
                return await _dbContext.Orderlijnens
                    .Include(ol => ol.Order)
                    .Include(ol => ol.Ticket)
                        .ThenInclude(t => t.Match)
                    .Where(ol =>
                        ol.Order.UserId == userId &&
                        ol.TicketId != null &&
                        ol.Ticket.MatchId != matchId)
                    .AnyAsync(ol =>
                        ol.Ticket.Match.DatumTijdStartMatch.Date == matchDatum.Date);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public Task UpdateAsync(Ticket entity)
        {
            throw new NotImplementedException();
        }
    }
}
