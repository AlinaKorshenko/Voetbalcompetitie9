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
    public class OrderlijnenService : IService<Orderlijnen>
    {
        private IDAO<Orderlijnen> _orderlijnenDAO;

        public OrderlijnenService(IDAO<Orderlijnen> orderlijnenDAO)
        {
            _orderlijnenDAO = orderlijnenDAO;
        }

        public async Task AddAsync(Orderlijnen entity)
        {
            await _orderlijnenDAO.AddAsync(entity);
        }

        public Task DeleteAsync(Orderlijnen entity)
        {
            throw new NotImplementedException();
        }

        public Task<Orderlijnen?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Orderlijnen>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Orderlijnen entity)
        {
            throw new NotImplementedException();
        }
    }
}
