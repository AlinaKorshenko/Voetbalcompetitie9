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
    public class TicketPrijsDAO : ITicketPrijsDAO
    {

        private readonly FootballDbContext _dbContext;

        public TicketPrijsDAO(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(TicketsPrijs entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TicketsPrijs entity)
        {
            throw new NotImplementedException();
        }

        public Task<TicketsPrijs?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TicketsPrijs>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<decimal> GetTicketPrijsByMatchAndSectionAsync(string MatchID, string VakNummer)
        {

            try {
                var ticket = await _dbContext.TicketsPrijs
                    .FirstOrDefaultAsync(tp => tp.MatchId == MatchID && tp.VakNummer == VakNummer);

                if (ticket == null)
                {
                    throw new Exception("Ticket prijs not found for the given MatchID and VakNummer.");
                }

                return ticket.Prijs;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public Task UpdateAsync(TicketsPrijs entity)
        {
            throw new NotImplementedException();
        }
    }
    }

