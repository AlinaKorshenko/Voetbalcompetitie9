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
    public class SeizoenenService : ISeizoenenService
    {
        private ISeizoenenDAO _seizoenenDAO;

        public SeizoenenService(ISeizoenenDAO seizoenenDAO)
        {
            _seizoenenDAO = seizoenenDAO;
        }

        public Task AddAsync(Seizoenen entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Seizoenen entity)
        {
            throw new NotImplementedException();
        }

        public Task<Seizoenen?> FindByIdAsync(string Id)
        {
            try
            {
                if(Id == null)
                {
                    throw new ArgumentNullException("Het meegegeven seizoenId mag niet null zijn.");
                }

                return _seizoenenDAO.FindByIdAsync(Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Het is niet gelukt om Seizoenen te vinden voor het meegegeven seizoenId '{Id}': ", ex);
            }
        }

        public async Task<IEnumerable<Seizoenen>?> GetAllAsync()
        {
            try
            {
                return await _seizoenenDAO.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Het is niet gelukt om alle Seizoenen op te halen: ", ex);
            }
        }

        public async Task<IEnumerable<Seizoenen>?> GetAllFutureSeasons()
        {
            try
            {
                return await _seizoenenDAO.GetAllFutureSeasons();
            }
            catch(Exception ex)
            {
                throw new Exception("Het is niet gelukt om alle toekomstige seizoenen op te halen: ", ex);
            }
        }
    }
}
