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
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("Het meegegeven Orderlijn mag niet null zijn.");
                }

                await _orderlijnenDAO.AddAsync(entity);
            }
            catch(Exception ex)
            {
                throw new Exception("Het toevoegen van het meegegeven Orderlijn is mislukt: ", ex);
            }
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
                throw new ApplicationException("Het verwijderen van het meegegeven Orderlijn is mislukt: ", ex);
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
