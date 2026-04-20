using ChampionsLeagueTickets.Domain.DataDB;
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
            catch
            {
                Console.WriteLine("error in DAO");
                throw;
            }
        }
}
    }

