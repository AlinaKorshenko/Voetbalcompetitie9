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
    public class MatchesDAO : IMatchDAO
    {

        private readonly FootballDbContext _dbContext;

        public MatchesDAO(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(Match entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Match entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Match?> FindByIdAsync(string Id)
        {
            try
            {
                var matches = await _dbContext.Matches
                    .Include(m => m.ThuisTeam)
                    .ThenInclude(t => t.Stadion)
                    .Include(m => m.BezoekendTeam)
                    .FirstOrDefaultAsync(m => m.MatchId == Id);

                if (matches == null)
                {
                    throw new Exception("Match met meegegeven id niet gevonden.");
                }

                return matches;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Match>?> GetAllAsync()
        {
            try
            {
                return await _dbContext.Matches
                    .Include(m => m.ThuisTeam)
                        .ThenInclude(t => t.Stadion)
                    .Include(m => m.BezoekendTeam)
                    .Where(m => m.DatumTijdStartMatch > DateTime.Now)
                    .OrderBy(m => m.DatumTijdStartMatch)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Match>?> GetAllMatchesFromTeamsAsync(string homeTeamId, string awayTeamId)
        {
            try
            {
                return await _dbContext.Matches
                    .Include(m => m.ThuisTeam)
                        .ThenInclude(t => t.Stadion)
                    .Include(m => m.BezoekendTeam)
                    .Where(match => match.ThuisTeamId == homeTeamId && match.BezoekendTeamId == awayTeamId)
                    .OrderBy(m => m.DatumTijdStartMatch)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Match>?> GetAllMatchesWhereUserIsAbleToBuyTickets()
        {
            try
            {
                var now = DateTime.Now;
                var oneMonthFromNow = now.AddMonths(1);

                return await _dbContext.Matches
                    .Include(m => m.ThuisTeam)
                        .ThenInclude(t => t.Stadion)
                    .Include(m => m.BezoekendTeam)
                    .Where(m =>
                        m.DatumTijdStartMatch > now &&
                        m.DatumTijdStartMatch <= oneMonthFromNow
                    )
                    .OrderBy(m => m.DatumTijdStartMatch)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public async Task<Stadion> GetStadionByIMatchdAsync(string matchId)
        {
            return await _dbContext.Matches
    .Where(m => m.MatchId == matchId)
    .Select(m => m.ThuisTeam.Stadion)
    .FirstOrDefaultAsync();
        }

        public Task UpdateAsync(Match entity)
        {
            throw new NotImplementedException();
        }
    }
}
