using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories;
using ChampionsLeagueTickets.Repositories.Interfaces;
using ChampionsLeagueTickets.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services
{
    public class OrderLijnenService : IOrderLijnenService
    {
        private IOrderLijnenDAO _orderlijnenDAO;

        public OrderLijnenService(IOrderLijnenDAO orderlijnenDAO)
        {
            _orderlijnenDAO = orderlijnenDAO;
        }

        public async Task AddAsync(Orderlijnen entity)
        {
            await _orderlijnenDAO.AddAsync(entity);
        }

        public async Task DeleteAsync(Orderlijnen entity)
        {
            try
            {
                var entityFound = await _orderlijnenDAO
                    .FindByOrderIdAndOrderLijnNumber(entity.OrderId, entity.OrderLijnNummer);

                if (entityFound == null)
                {
                    throw new Exception("Orderlijn niet gevonden");
                }

                await _orderlijnenDAO.DeleteAsync(entity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Fout bij verwijderen van orderlijn", ex);
            }
        }

        public Task<Orderlijnen?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Orderlijnen>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Orderlijnen> FindByOrderIdAndOrderLijnNumber(string orderId, int orderLijnNumber)
        {
            return await _orderlijnenDAO.FindByOrderIdAndOrderLijnNumber(orderId, orderLijnNumber);
        }
    }
}
