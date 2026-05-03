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
    public class OrderService : IOrderService
    {

        private IOrderDAO _orderDAO;

        public OrderService(IOrderDAO orderDAO)
        {
            _orderDAO = orderDAO;
        }

        public async Task AddAsync(Order entity)
        {
            await _orderDAO.AddAsync(entity);
        }

        public Task DeleteAsync(Order entity)
        {
            return _orderDAO.DeleteAsync(entity);
        }

        public Task<Order?> FindByIdAsync(string Id)
        {
            return _orderDAO.FindByIdAsync(Id);
        }

        public async Task<string> GenerateNextOrderIdAsync()
        {
            return await _orderDAO.GenerateNextOrderIdAsync();
        }

        public Task<IEnumerable<Order>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetAllByUserId(string userId)
        public Task<IEnumerable<Order>?> GetAllOrderInformationFromUser(string userId)
        {
           return _orderDAO.GetAllByUserId(userId);

            throw new NotImplementedException();
        }

        public Task UpdateAsync(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
