using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories;
using ChampionsLeagueTickets.Repositories.integrations.VrijeZitplaats.DTO;
using ChampionsLeagueTickets.Repositories.Interfaces;
using ChampionsLeagueTickets.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services
{
    public class ZitplaatsenService : IZitplaatsenService
    {
        private IZitplaatsenDAO _zitplaatsenDAO;

        public ZitplaatsenService(IZitplaatsenDAO zitplaatsenDAO)
        {
            _zitplaatsenDAO = zitplaatsenDAO;
        }

        public Task AddAsync(Zitplaatsen entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Zitplaatsen entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Zitplaatsen?> FindByIdAsync(string Id)
        {
            return await _zitplaatsenDAO.FindByIdAsync(Id);
        }

        public async Task<IEnumerable<Zitplaatsen>?> GetAllAsync()
        {
            return await _zitplaatsenDAO.GetAllAsync();
        }

        public async Task<int> GetCountZitplaatsenByVakTypeAndStadion(Stadion stadion, VakType vaktype)
        {
            return await _zitplaatsenDAO.GetCountZitplaatsenByVakTypeAndStadion(stadion, vaktype);
        }

        public Task<List<string>> GetRowsForMatchAndSectionAsync(string matchId, string vakNummer)
        {
            return _zitplaatsenDAO.GetRowsForMatchAndSectionAsync(matchId, vakNummer);
        }

        public Task<List<ZitplaatsDto>> GetSeatsForMatchSectionAndRowAsync(string matchId, string vakNummer, string rijNummer)
        {
            return _zitplaatsenDAO.GetSeatsForMatchSectionAndRowAsync(matchId, vakNummer, rijNummer);
        }

        public Task UpdateAsync(Zitplaatsen entity)
        {
            throw new NotImplementedException();
        }
    }
}
