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
    public class AbonnementenPrijsDAO : IAbonementenPrijsDAO
    {
        private readonly FootballDbContext _dbContext;

        public AbonnementenPrijsDAO(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(AbonnementenPrijs entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(AbonnementenPrijs entity)
        {
            throw new NotImplementedException();
        }

        public Task<AbonnementenPrijs?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AbonnementenPrijs>?> GetAllAsync()
        {
            try
            {
                return await _dbContext.AbonnementenPrijs
                    .Include(ap => ap.Stadion)
                    .Include(ap => ap.Seizoen)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public async Task<decimal> GetPriceBySeizoenIdAndStadionId(string seizoenID, string stadionId)
        {

            try
            {
                var ticket = await _dbContext.AbonnementenPrijs
                    .FirstOrDefaultAsync(tp => tp.SeizoenId == seizoenID && tp.StadionId == stadionId);

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

        public Task UpdateAsync(AbonnementenPrijs entity)
        {
            throw new NotImplementedException();
        }
    }
}
