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
            return _stadionDAO.FindByIdAsync(Id);    
        }

        public async Task<IEnumerable<Stadion>?> GetAllAsync()
        {
            return await _stadionDAO.GetAllAsync();
        }

        public Task UpdateAsync(Stadion entity)
        {
            throw new NotImplementedException();
        }
    }
}
