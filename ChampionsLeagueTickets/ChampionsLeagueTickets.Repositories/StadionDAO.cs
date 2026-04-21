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
    public class StadionDAO : IDAO<Stadion>
    {
        private readonly FootballDbContext _dbContext;

        public StadionDAO(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(Stadion entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Stadion entity)
        {
            throw new NotImplementedException();
        }

        public Task<Stadion?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Stadion>?> GetAllAsync()
        {
            try
            {
                return await _dbContext.Stadions
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public Task UpdateAsync(Stadion entity)
        {
            throw new NotImplementedException();
        }
    }
}
