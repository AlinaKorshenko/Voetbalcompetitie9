using ChampionsLeagueTickets.Domain.DataDB;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Repositories
{
    public class OrderDAO: IOrderDAO
    {
        private readonly FootballDbContext _dbContext;

        public OrderDAO(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Order entity)
        {
            try
            {
                await _dbContext.Orders.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public Task DeleteAsync(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task<Order?> FindByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GenerateNextOrderIdAsync()
        {
            try
            {
                var lastOrder = await _dbContext.Orders
                    .OrderByDescending(o => o.OrderId)
                    .FirstOrDefaultAsync();

                if (lastOrder == null)
                    return "O0001";

                var lastNumber = int.Parse(lastOrder.OrderId.Substring(1));
                var newNumber = lastNumber + 1;

                return $"O{newNumber.ToString("D4")}";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DAO: " + ex.Message);
                throw;
            }
        }

        public Task<IEnumerable<Order>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>?> GetAllOrderInformationFromUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
