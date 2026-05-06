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
            try
            {
                if(entity == null)
                {
                    throw new ArgumentNullException("Het meegegeven Order mag niet null zijn.");
                }

                await _orderDAO.AddAsync(entity);
            }
            catch(Exception ex)
            {
                throw new Exception("Er is iets misgegaan bij het toevoegen van een Order: ", ex);
            }
        }

        public Task DeleteAsync(Order entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("Het meegegeven Order mag niet null zijn.");
                }

                return _orderDAO.DeleteAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Er is iets misgegaan bij het verwijderen van een Order: ", ex);
            }
        }

        public Task<Order?> FindByIdAsync(string Id)
        {
            try
            {
                if(Id == null)
                {
                    throw new ArgumentNullException("Het meegegeven orderId mag niet null zijn.");
                }

                return _orderDAO.FindByIdAsync(Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Het Order is niet gevonden voor het meegegeven orderId '{Id}': ", ex);
            }
        }

        public async Task<string> GenerateNextOrderIdAsync()
        {
            try
            {
                return await _orderDAO.GenerateNextOrderIdAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Het genereren van het volgende orderId is mislukt: ", ex);
            }
        }

        public Task<IEnumerable<Order>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetAllByUserId(string userId) {
            try
            {
                if(userId == null)
                {
                    throw new ArgumentNullException("Het meegegeven userId mag niet null zijn.");
                }

                return _orderDAO.GetAllByUserId(userId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Het is mislukt om de orders te vinden voor het userId '{userId}': ", ex);
            }
        }
    }
}
