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
    public class StadionService : IService<Stadion>
    {
        private IDAO<Stadion> _stadionDAO;

        public StadionService(IDAO<Stadion> stadionDAO)
        {
            _stadionDAO = stadionDAO;
        }

        public Task AddAsync(Stadion entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Stadion entity)
        {
            throw new NotImplementedException();
        }

        public Task<Stadion?> FindByIdAsync(string Id)
        {
            try
            {
                if (Id == null)
                {
                    throw new ArgumentNullException("Het meegegeven stadionId mag niet null zijn.");
                }

                return _stadionDAO.FindByIdAsync(Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Het is niet gelukt om het Stadion te vinden met meegegeven stadionId '{Id}': ", ex)
            }
        }

        public async Task<IEnumerable<Stadion>?> GetAllAsync()
        {
            try
            {
                return await _stadionDAO.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Het is niet gelukt om alle Stadions op te halen.");
            }
        }
    }
}
