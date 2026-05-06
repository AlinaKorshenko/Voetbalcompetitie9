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


    public class TeamDAO : IDAO<Team>
    {

        private readonly FootballDbContext _dbContext;

        public TeamDAO(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(Team entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Team entity)
        {
            throw new NotImplementedException();
        }

        public Task<Team?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Team>?> GetAllAsync()
        {
            return await _dbContext.Teams
                .Include(t => t.Stadion)
                .ToListAsync();
        }
    }
}
