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
            return await _dbContext.AbonnementenPrijs
                .Include(ap => ap.Stadion)
                .Include(ap => ap.Seizoen)
                .Include(ap => ap.VakNummerNavigation)
                .OrderByDescending(ap => ap.Seizoen.StartDatum)
                .ToListAsync();
        }

        public async Task<IEnumerable<AbonnementenPrijs>?> GetAllPrijzenFromNextSeasons()
        {
            var vandaag = DateOnly.FromDateTime(DateTime.Now);

            return await _dbContext.AbonnementenPrijs
                .Where(a => a.Seizoen.StartDatum > vandaag)
                                    .Include(ap => ap.Stadion)
                .Include(ap => ap.Seizoen)
                .Include(ap => ap.VakNummerNavigation)
                .OrderBy(ap => ap.Seizoen.StartDatum)
                .ToListAsync();
        }

        public async Task<decimal> GetPriceBySeizoenIdAndStadionId(string seizoenID, string stadionId)
        {
            var ticket = await _dbContext.AbonnementenPrijs
                .FirstOrDefaultAsync(tp => tp.SeizoenId == seizoenID && tp.StadionId == stadionId);

            return ticket.Prijs;
        }
    }
}
