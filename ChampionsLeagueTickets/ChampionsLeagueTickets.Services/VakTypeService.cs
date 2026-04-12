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
    public class VakTypeService : IService<VakType>
    {
        private IDAO<VakType> _vakTypeDAO;

        public VakTypeService(IDAO<VakType> vakTypeDAO)
        {
            _vakTypeDAO = vakTypeDAO;
        }

        public Task AddAsync(VakType entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(VakType entity)
        {
            throw new NotImplementedException();
        }

        public Task<VakType?> FindByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VakType>?> GetAllAsync()
        {
            return await _vakTypeDAO.GetAllAsync();
        }

        public Task UpdateAsync(VakType entity)
        {
            throw new NotImplementedException();
        }
    }
}
