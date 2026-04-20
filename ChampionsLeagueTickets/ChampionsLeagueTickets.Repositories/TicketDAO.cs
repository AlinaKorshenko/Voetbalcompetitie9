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

        public async Task<Ticket?> FindByMatchAndSeatAsync(string matchId, string seatId)
        {
            try
            {
                return await _dbContext.Tickets
                    .FirstOrDefaultAsync(t => t.MatchId == matchId && t.ZitplaatsId == seatId);
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Ticket>?> GetAllAsync()
        {
            return await _dbContext.Tickets
                     
                   .ToListAsync();
        }

        public Task UpdateAsync(Ticket entity)
        {
            throw new NotImplementedException();
        }
    }
}
