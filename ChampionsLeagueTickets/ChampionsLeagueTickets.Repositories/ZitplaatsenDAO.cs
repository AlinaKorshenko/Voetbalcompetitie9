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
    public class ZitplaatsenDAO : IZitplaatsenDAO
    {
        private readonly FootballDbContext _dbContext;

        public ZitplaatsenDAO(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(Zitplaatsen entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Zitplaatsen entity)
        {
            throw new NotImplementedException();
        }

        public Task<Zitplaatsen?> FindByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Zitplaatsen>?> GetAllAsync()
        {
            try
            {
                return await _dbContext.Zitplaatsens
                    .ToListAsync();
            }
            catch
            {
                Console.WriteLine("error in DAO");
                throw;
            }
        }

        public async Task<int> GetCountZitplaatsenByVakTypeAndStadion(Stadion stadion, VakType vaktype)
        {
            try
            {
                return await _dbContext.Zitplaatsens
                    .Where(zitplaats => zitplaats.StadionId == stadion.StadionId && zitplaats.VakNummer == vaktype.VakNummer)
                    .CountAsync();
            }
            catch
            {
                Console.WriteLine("error in DAO");
                throw;
            }
        }

        public Task UpdateAsync(Zitplaatsen entity)
        {
            throw new NotImplementedException();
        }
    }
}
