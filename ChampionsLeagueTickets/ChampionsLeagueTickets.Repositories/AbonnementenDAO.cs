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
            try
            {
                await _dbContext.Abonnementens.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public Task DeleteAsync(Abonnementen entity)
        {
            throw new NotImplementedException();
        }

        public async Task<string> FindAbinementIdByStadonSeizoenZitplaatsAsynk(string stadionId, string seizoenId, string zitplaatsId)
        {

            try
            {
                return await _dbContext.Abonnementens
                    .Where(a =>
                        a.StadionId == stadionId &&
                        a.SeizoenId == seizoenId &&
                        a.ZitplaatsId == zitplaatsId)
                    .Select(a => a.AbonnementId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public async Task<Abonnementen> FindAbonnementByStadionIdAndAbonnementId(string abonnementId, string stadionId)
        {
            return await _dbContext.Abonnementens
      .Include(a => a.Zitplaatsen) 
      .Include(b=> b.Stadion)
      .FirstOrDefaultAsync(a => a.AbonnementId == abonnementId
                             && a.StadionId == stadionId);
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
            catch(Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public Task UpdateAsync(Abonnementen entity)
        {
            throw new NotImplementedException();
        }
    }
}
