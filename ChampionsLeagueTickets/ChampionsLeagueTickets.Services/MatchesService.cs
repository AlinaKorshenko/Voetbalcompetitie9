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
            try
            {
                if (Id == null)
                {
                    throw new ArgumentNullException("Het meegegeven matchId mag niet null zijn.");
                }

                return await _matchesDAO.FindByIdAsync(Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"De match is niet gevonden voor het meegegeven matchId '{Id}': ", ex);
            }
        }

        public async Task<IEnumerable<Match>?> GetAllAsync()
        {
            try
            {
                return await _matchesDAO.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Gefaald om alle matches op te halen: ", ex);
            }
        }

        public async Task<IEnumerable<Match>?> GetAllMatchesFromTeamsAsync(string homeTeamId, string awayTeamId)
        {
            try
            {
                if (homeTeamId == null)
                {
                    throw new ArgumentNullException("Het meegegeven homeTeamId mag niet null zijn.");
                }

                if (awayTeamId == null)
                {
                    throw new ArgumentNullException("Het meegegeven awayTeamId mag niet null zijn.");
                }

                return await _matchesDAO.GetAllMatchesFromTeamsAsync(homeTeamId, awayTeamId);
            }
            catch(Exception ex)
            {
                throw new Exception($"Er zijn geen matches gevonden voor de meegegeven teams '{homeTeamId}' en '{awayTeamId}'");
            }
        }

        public async Task<Stadion> GetStadionByMatchIdAsync(string matchId)
        {
            try
            {
                if (matchId == null)
                {
                    throw new ArgumentNullException("Het meegegeven matchId mag niet null zijn");
                }

                return await _matchesDAO.GetStadionByMatchIdAsync(matchId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Er zijn geen stadions gevonden voor het meegegeven matchId '{matchId}':", ex);
            }
        }
    }
}
