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
    public class AbonnementenDAO : IAbonnementDAO
    {
        private readonly FootballDbContext _dbContext;

        public AbonnementenDAO(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Abonnementen entity)
        {
            await _dbContext.Abonnementens.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Abonnementen entity)
        {
            _dbContext.Abonnementens.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public Task<Abonnementen?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GenerateNextAbonnementenIdAsync()
        {
            var lastAbonnement = await _dbContext.Abonnementens
                .OrderByDescending(o => o.AbonnementId)
                .FirstOrDefaultAsync();

            if (lastAbonnement == null)
            {
                return "A0001";
            }

            var lastNumber = int.Parse(lastAbonnement.AbonnementId.Substring(1));
            var newNumber = lastNumber + 1;

            return $"A{newNumber.ToString("D4")}";
        }

        public async Task<IEnumerable<Abonnementen>?> GetAllAsync()
        {
            return await _dbContext.Abonnementens
                .ToListAsync();
        }
    }
}
