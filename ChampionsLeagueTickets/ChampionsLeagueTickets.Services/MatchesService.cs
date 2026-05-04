using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories.Interfaces;
using ChampionsLeagueTickets.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services
{
    public class MatchesService : IMatchService
    {
        private IMatchDAO _matchesDAO;

        public MatchesService(IMatchDAO matchDAO)
        {
            _matchesDAO = matchDAO;
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
            return await _matchesDAO.FindByIdAsync(Id);
        }

        public async Task<IEnumerable<Match>?> GetAllAsync()
        {
            return await _matchesDAO.GetAllAsync();
        }

        public async Task<IEnumerable<Match>?> GetAllMatchesFromTeamsAsync(string homeTeamId, string awayTeamId)
        {
            return await _matchesDAO.GetAllMatchesFromTeamsAsync(homeTeamId, awayTeamId);
        }

        public async Task<Stadion> GetStadionByMatchIdAsync(string matchId)
        {
            return await _matchesDAO.GetStadionByMatchIdAsync(matchId);
        }
    }
}
