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
    public class AbonnementenService : IService<Abonnementen>
    {
        private IDAO<Abonnementen> _abonnementenDAO;

        public AbonnementenService(IDAO<Abonnementen> abonnementenDAO)
        {
            _abonnementenDAO = abonnementenDAO;
        }

        public Task AddAsync(Abonnementen entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Abonnementen entity)
        {
            throw new NotImplementedException();
        }

        public Task<Abonnementen?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
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
