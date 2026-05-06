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
    public class SeizoenenDAO : ISeizoenenDAO
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
            var seizoen = await _dbContext.Seizoenens
                .FirstOrDefaultAsync(s => s.SeizoenId == Id);

            return seizoen;
        }

        public async Task<IEnumerable<Seizoenen>?> GetAllAsync()
        {
            return await _dbContext.Seizoenens
                .OrderByDescending(s => s.StartDatum)
                .ToListAsync();
        }

        public async Task<IEnumerable<Seizoenen>?> GetAllFutureSeasons()
        {
            var vandaag = DateOnly.FromDateTime(DateTime.Now);

            return await _dbContext.Seizoenens
                .Where(s => s.StartDatum > vandaag)
                .OrderByDescending(s => s.StartDatum)
                .ToListAsync();
        }
    }
}
