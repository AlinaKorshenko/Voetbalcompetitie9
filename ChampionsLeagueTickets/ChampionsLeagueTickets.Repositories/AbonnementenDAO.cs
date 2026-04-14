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
    public class AbonnementenDAO : IDAO<Abonnementen>
    {
        private readonly FootballDbContext _dbContext;

        public AbonnementenDAO(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(Abonnementen entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Abonnementen entity)
        {
            throw new NotImplementedException();
        }

        public Task<Abonnementen?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Abonnementen>?> GetAllAsync()
        {
            try
            {
                return await _dbContext.Abonnementens
                    .ToListAsync();
            }
            catch
            {
                Console.WriteLine("error in DAO");
                throw;
            }
        }

        public Task UpdateAsync(Abonnementen entity)
        {
            throw new NotImplementedException();
        }
    }
}
