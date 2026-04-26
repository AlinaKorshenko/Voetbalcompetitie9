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
           return _seizoenenDAO.FindByIdAsync(Id);
        }

        public async Task<IEnumerable<Seizoenen>?> GetAllAsync()
        {
            return await _seizoenenDAO.GetAllAsync();
        }

        public async Task<IEnumerable<Seizoenen>?> GetCurrentSeizoen()
        {
            return await _seizoenenDAO.GetCurrentSeizoen();
        }

        public Task UpdateAsync(Seizoenen entity)
        {
            throw new NotImplementedException();
        }
    }
}
