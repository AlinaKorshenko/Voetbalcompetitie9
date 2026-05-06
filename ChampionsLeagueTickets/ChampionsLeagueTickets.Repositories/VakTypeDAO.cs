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
    public class VakTypeDAO : IDAO<VakType>
    {
        private readonly FootballDbContext _dbContext;

        public VakTypeDAO(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(VakType entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(VakType entity)
        {
            throw new NotImplementedException();
        }

        public async Task<VakType?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VakType>?> GetAllAsync()
        {
            try
            {
                return await _dbContext.VakTypes
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
