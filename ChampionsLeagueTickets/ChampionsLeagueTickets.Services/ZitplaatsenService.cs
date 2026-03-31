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
    public class ZitplaatsenService : IService<Zitplaatsen>
    {
        private IDAO<Zitplaatsen> _zitplaatsenDAO;

        public ZitplaatsenService(IDAO<Zitplaatsen> zitplaatsenDAO)
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

        public Task<Zitplaatsen?> FindByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Zitplaatsen>?> GetAllAsync()
        {
            return await _zitplaatsenDAO.GetAllAsync();
        }

        public Task UpdateAsync(Zitplaatsen entity)
        {
            throw new NotImplementedException();
        }
    }
}
