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
    public class OrderLijnService : IOrderLijnService
    {
        private IOrderLijnDAO _orderLijnDAO;


        public OrderLijnService(IOrderLijnDAO orderLijnDAO) {
            _orderLijnDAO = orderLijnDAO;
        
        }

        public async Task AddAsync(Orderlijnen entity)
        {
            await _orderLijnDAO.AddAsync(entity);

        }

        public async Task DeleteAsync(Orderlijnen entity)
        {
            await _orderLijnDAO.DeleteAsync(entity);
        }

        public Task<Orderlijnen?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Orderlijnen> FindByOrderIdAndOrderLijnNumber(string orderId, int orderLijnNumber)
        {
            return await _orderLijnDAO.FindByOrderIdAndOrderLijnNumber(orderId, orderLijnNumber);

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
