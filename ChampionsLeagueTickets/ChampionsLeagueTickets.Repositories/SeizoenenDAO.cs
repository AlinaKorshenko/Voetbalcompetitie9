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
    public class SeizoenenDAO : IDAO<Seizoenen>
    {
        private readonly FootballDbContext _dbContext;

        public SeizoenenDAO(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(Seizoenen entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Seizoenen entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Seizoenen?> FindByIdAsync(string Id)
        {
            try
            {
                var seizoen = await _dbContext.Seizoenens
                    .FirstOrDefaultAsync(s => s.SeizoenId == Id);

                if(seizoen == null)
                {
                    throw new Exception("Seizoen not found for the given SeizoenId.");
                }

                return seizoen;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Seizoenen>?> GetAllAsync()
        {
            try
            {
                return await _dbContext.Seizoenens
                    .OrderByDescending(s => s.StartDatum)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }
    }
}
