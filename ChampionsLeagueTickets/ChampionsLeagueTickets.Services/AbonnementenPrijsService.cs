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
            try
            {
                return await _abonnementenPrijsDAO.GetAllAsync();
            } 
            catch(Exception ex)
            {
                throw new Exception("Gefaald om abonnementen prijzen te laden: ", ex);
            }
        }

        public async Task<IEnumerable<AbonnementenPrijs>?> GetAllPrijzenFromNextSeasons()
        {
            try
            {
                return await _abonnementenPrijsDAO.GetAllPrijzenFromNextSeasons();
            }
            catch (Exception ex)
            {
                throw new Exception("Gefaald om abonnementen prijzen te laden voor volgende seizoenen: ", ex);
            }
        }

        public async Task<decimal> GetPriceBySeizoenIdAndStadionId(string seizoenID, string stadionId)
        {
            try
            {
                if(seizoenID == null)
                {
                    throw new ArgumentNullException("Het meegegeven seizoenId mag niet null zijn.");
                }

                if (stadionId == null)
                {
                    throw new ArgumentNullException("Het meegegeven stadionId mag niet null zijn.");
                }

                return await _abonnementenPrijsDAO.GetPriceBySeizoenIdAndStadionId(seizoenID, stadionId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Kon geen prijs ophalen voor seizoenId '{seizoenID}' en stadionId '{stadionId}': ", ex);
            }
        }
    }
}
