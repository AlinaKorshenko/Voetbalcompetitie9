using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories;
using ChampionsLeagueTickets.Repositories.Interfaces;
using ChampionsLeagueTickets.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services
{
    public class AbonnementenService : IAbonnementService
    {
        private IAbonnementDAO _abonnementenDAO;

        public AbonnementenService(IAbonnementDAO abonnementenDAO)
        {
            _abonnementenDAO = abonnementenDAO;
        }

        public async Task AddAsync(Abonnementen entity)
        {
            await _abonnementenDAO.AddAsync(entity);
        }

        public async Task DeleteAsync(Abonnementen entity)
        {
            await _abonnementenDAO.DeleteAsync(entity);
        }

        public async Task<string> FindAbinementIdByStadonSeizoenZitplaatsAsynk(string stadionId, string seizoenId, string zitplaatsId)
        {
            return await _abonnementenDAO.FindAbinementIdByStadonSeizoenZitplaatsAsynk(stadionId, seizoenId, zitplaatsId);
        }

        public Task<Abonnementen> FindAbonnementByStadionIdAndAbonnementId(string abonnementId, string stadionId)
        {
            throw new NotImplementedException();
        }

        public Task<Abonnementen?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GenerateNextAbonnementenIdAsync()
        {
            return await _abonnementenDAO.GenerateNextAbonnementenIdAsync();
        }

        public async Task<IEnumerable<Abonnementen>?> GetAllAsync()
        {
            return await _abonnementenDAO.GetAllAsync();
        }

        public Task UpdateAsync(Abonnementen entity)
        {
            throw new NotImplementedException();
        }
    }
}
