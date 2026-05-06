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
    public class AbonnementenPrijsService : IAbonementenPrijsService
    {
        private IAbonementenPrijsDAO _abonnementenPrijsDAO;

        public AbonnementenPrijsService(IAbonementenPrijsDAO abonnementenPrijsDAO)
        {
            _abonnementenPrijsDAO = abonnementenPrijsDAO;
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
            return await _abonnementenPrijsDAO.GetAllAsync();
        }

        public async Task<IEnumerable<AbonnementenPrijs>?> GetAllPrijzenFromNextSeasons()
        {
            return await _abonnementenPrijsDAO.GetAllPrijzenFromNextSeasons();
        }

        public async Task<decimal> GetPriceBySeizoenIdAndStadionId(string seizoenID, string stadionId)
        {
            return await _abonnementenPrijsDAO.GetPriceBySeizoenIdAndStadionId(seizoenID, stadionId);
        }
    }
}
